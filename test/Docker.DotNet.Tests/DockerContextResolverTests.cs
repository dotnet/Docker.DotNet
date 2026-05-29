using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Xunit;

// These tests mutate process-wide DOCKER_* environment variables, so they must
// not run in parallel with the daemon-backed integration tests (whose fixture
// reads those variables when it constructs a DockerClientConfiguration).
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Docker.DotNet.Tests
{
    public class DockerContextResolverTests
    {
        private static readonly string PlatformDefault =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "npipe://./pipe/docker_engine"
                : "unix:/var/run/docker.sock";

        [Fact]
        public void Default_constructor_honors_DOCKER_HOST()
        {
            using var env = new DockerEnvironment();
            env.Set("DOCKER_HOST", "tcp://1.2.3.4:2375");

            using var configuration = new DockerClientConfiguration();

            Assert.Equal(new Uri("tcp://1.2.3.4:2375"), configuration.EndpointBaseUri);
        }

        [Fact]
        public void Default_constructor_upgrades_tcp_to_https_when_DOCKER_TLS_VERIFY_set()
        {
            using var env = new DockerEnvironment();
            env.Set("DOCKER_HOST", "tcp://1.2.3.4:2376");
            env.Set("DOCKER_TLS_VERIFY", "1");

            using var configuration = new DockerClientConfiguration();

            Assert.Equal(new Uri("https://1.2.3.4:2376"), configuration.EndpointBaseUri);
        }

        [Fact]
        public void Default_constructor_falls_back_to_platform_default()
        {
            using var env = new DockerEnvironment();
            // Empty config directory, no DOCKER_HOST/DOCKER_CONTEXT: nothing to resolve.

            using var configuration = new DockerClientConfiguration();

            Assert.Equal(new Uri(PlatformDefault), configuration.EndpointBaseUri);
        }

        [Fact]
        public void Default_constructor_resolves_currentContext_from_config_json()
        {
            using var env = new DockerEnvironment();
            env.WriteContext("remote", "tcp://10.0.0.5:2375");
            env.WriteCurrentContext("remote");

            using var configuration = new DockerClientConfiguration();

            Assert.Equal(new Uri("tcp://10.0.0.5:2375"), configuration.EndpointBaseUri);
        }

        [Fact]
        public void Default_constructor_prefers_DOCKER_CONTEXT_over_config_json()
        {
            using var env = new DockerEnvironment();
            env.WriteContext("from-config", "tcp://10.0.0.5:2375");
            env.WriteCurrentContext("from-config");
            env.WriteContext("from-env", "tcp://10.0.0.9:2375");
            env.Set("DOCKER_CONTEXT", "from-env");

            using var configuration = new DockerClientConfiguration();

            Assert.Equal(new Uri("tcp://10.0.0.9:2375"), configuration.EndpointBaseUri);
        }

        [Fact]
        public void Default_constructor_normalizes_windows_named_pipe_context()
        {
            // Docker stores Windows pipe endpoints in Go's native form. The
            // resolved endpoint must be normalized to the form DockerClient's
            // npipe handler accepts (npipe://./pipe/<name>), otherwise creating a
            // client throws "is not a valid npipe URI".
            using var env = new DockerEnvironment();
            env.WriteContext("desktop-linux", "npipe:////./pipe/dockerDesktopLinuxEngine");
            env.WriteCurrentContext("desktop-linux");

            using var configuration = new DockerClientConfiguration();

            Assert.Equal(new Uri("npipe://./pipe/dockerDesktopLinuxEngine"), configuration.EndpointBaseUri);

            // Must not throw - this is the failure the normalization fixes.
            using var client = configuration.CreateClient();
        }

        [Fact]
        public void FromContext_resolves_named_context()
        {
            using var env = new DockerEnvironment();
            env.WriteContext("remote", "tcp://10.0.0.5:2375");

            using var configuration = DockerClientConfiguration.FromContext("remote");

            Assert.Equal(new Uri("tcp://10.0.0.5:2375"), configuration.EndpointBaseUri);
        }

        [Fact]
        public void FromContext_default_returns_platform_default()
        {
            using var env = new DockerEnvironment();

            using var configuration = DockerClientConfiguration.FromContext("default");

            Assert.Equal(new Uri(PlatformDefault), configuration.EndpointBaseUri);
        }

        [Fact]
        public void FromContext_unknown_context_throws()
        {
            using var env = new DockerEnvironment();

            Assert.Throws<InvalidOperationException>(() => DockerClientConfiguration.FromContext("does-not-exist"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void FromContext_requires_a_context_name(string contextName)
        {
            using var env = new DockerEnvironment();

            Assert.Throws<ArgumentException>(() => DockerClientConfiguration.FromContext(contextName));
        }

        /// <summary>
        /// Snapshots and clears the DOCKER_* variables the resolver reads, points
        /// DOCKER_CONFIG at an isolated temp directory, and restores everything on
        /// dispose so each test sees a clean, deterministic environment.
        /// </summary>
        private sealed class DockerEnvironment : IDisposable
        {
            private static readonly string[] Names =
            {
                "DOCKER_HOST", "DOCKER_TLS_VERIFY", "DOCKER_CONTEXT", "DOCKER_CONFIG"
            };

            private readonly (string Name, string Value)[] _saved;
            private readonly string _configDirectory;

            public DockerEnvironment()
            {
                _saved = Array.ConvertAll(Names, name => (name, Environment.GetEnvironmentVariable(name)));

                foreach (var name in Names)
                {
                    Environment.SetEnvironmentVariable(name, null);
                }

                _configDirectory = Path.Combine(Path.GetTempPath(), "docker.dotnet.tests", Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(_configDirectory);
                Environment.SetEnvironmentVariable("DOCKER_CONFIG", _configDirectory);
            }

            public void Set(string name, string value)
            {
                Environment.SetEnvironmentVariable(name, value);
            }

            public void WriteCurrentContext(string contextName)
            {
                File.WriteAllText(
                    Path.Combine(_configDirectory, "config.json"),
                    "{\"currentContext\":\"" + contextName + "\"}");
            }

            public void WriteContext(string contextName, string host)
            {
                var metaDirectory = Path.Combine(_configDirectory, "contexts", "meta", Sha256Hex(contextName));
                Directory.CreateDirectory(metaDirectory);
                File.WriteAllText(
                    Path.Combine(metaDirectory, "meta.json"),
                    "{\"Name\":\"" + contextName + "\",\"Endpoints\":{\"docker\":{\"Host\":\"" + host + "\"}}}");
            }

            public void Dispose()
            {
                foreach (var (name, value) in _saved)
                {
                    Environment.SetEnvironmentVariable(name, value);
                }

                try
                {
                    Directory.Delete(_configDirectory, recursive: true);
                }
                catch (IOException)
                {
                }
            }

            private static string Sha256Hex(string value)
            {
                using var sha = SHA256.Create();
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                var builder = new StringBuilder(bytes.Length * 2);
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
