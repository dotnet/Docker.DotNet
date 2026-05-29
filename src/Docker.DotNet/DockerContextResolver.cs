using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Docker.DotNet
{
    /// <summary>
    /// Resolves the Docker daemon endpoint the same way the docker CLI does:
    /// environment variables first, then the current Docker context, then the
    /// platform default socket. Works on Linux, macOS, and Windows.
    /// </summary>
    internal static class DockerContextResolver
    {
        private const string DefaultContextName = "default";
        private const string DockerEndpointKey = "docker";

        public static Uri Resolve()
        {
            var host = Environment.GetEnvironmentVariable("DOCKER_HOST");
            if (!string.IsNullOrEmpty(host))
            {
                return BuildHostUri(host, IsTlsVerifyEnabled());
            }

            var contextName = Environment.GetEnvironmentVariable("DOCKER_CONTEXT");
            if (string.IsNullOrEmpty(contextName))
            {
                contextName = TryReadCurrentContext();
            }

            if (!string.IsNullOrEmpty(contextName) && !string.Equals(contextName, DefaultContextName, StringComparison.Ordinal))
            {
                var contextHost = TryReadContextHost(contextName);
                if (!string.IsNullOrEmpty(contextHost))
                {
                    return BuildHostUri(contextHost, IsTlsVerifyEnabled());
                }
            }

            return GetPlatformDefault();
        }

        public static Uri ResolveForContext(string contextName)
        {
            if (string.IsNullOrEmpty(contextName))
            {
                throw new ArgumentException("Context name must be provided", nameof(contextName));
            }

            if (string.Equals(contextName, DefaultContextName, StringComparison.Ordinal))
            {
                return GetPlatformDefault();
            }

            var host = TryReadContextHost(contextName);
            if (string.IsNullOrEmpty(host))
            {
                throw new InvalidOperationException($"Docker context '{contextName}' was not found under '{GetContextsMetaDirectory()}'.");
            }

            return BuildHostUri(host, tlsVerify: false);
        }

        private static bool IsTlsVerifyEnabled()
        {
            var value = Environment.GetEnvironmentVariable("DOCKER_TLS_VERIFY");
            return !string.IsNullOrEmpty(value);
        }

        private static Uri BuildHostUri(string host, bool tlsVerify)
        {
            if (tlsVerify && host.StartsWith("tcp://", StringComparison.OrdinalIgnoreCase))
            {
                host = "https://" + host.Substring("tcp://".Length);
            }

            host = NormalizeNpipe(host);

            return new Uri(host);
        }

        // Docker stores Windows named-pipe endpoints in Go's native form
        // (npipe:////./pipe/<name>), but DockerClient's npipe handler expects
        // npipe://./pipe/<name>. Collapse the former (and the localhost variant)
        // to the latter so resolved contexts are usable on Windows.
        private static string NormalizeNpipe(string host)
        {
            const string scheme = "npipe://";
            if (!host.StartsWith(scheme, StringComparison.OrdinalIgnoreCase))
            {
                return host;
            }

            var pipe = host.Substring(scheme.Length).TrimStart('/');
            if (pipe.StartsWith("./", StringComparison.Ordinal))
            {
                pipe = pipe.Substring("./".Length);
            }
            else if (pipe.StartsWith("localhost/", StringComparison.OrdinalIgnoreCase))
            {
                pipe = pipe.Substring("localhost/".Length);
            }

            return scheme + "./" + pipe;
        }

        private static string TryReadCurrentContext()
        {
            var configPath = Path.Combine(GetDockerConfigDirectory(), "config.json");
            if (!File.Exists(configPath))
            {
                return null;
            }

            try
            {
                using var stream = File.OpenRead(configPath);
                using var document = JsonDocument.Parse(stream);
                if (document.RootElement.TryGetProperty("currentContext", out var element) && element.ValueKind == JsonValueKind.String)
                {
                    return element.GetString();
                }
            }
            catch (JsonException)
            {
            }
            catch (IOException)
            {
            }

            return null;
        }

        private static string TryReadContextHost(string contextName)
        {
            var metaPath = Path.Combine(GetContextsMetaDirectory(), Sha256Hex(contextName), "meta.json");
            if (!File.Exists(metaPath))
            {
                return null;
            }

            try
            {
                using var stream = File.OpenRead(metaPath);
                using var document = JsonDocument.Parse(stream);
                if (!document.RootElement.TryGetProperty("Endpoints", out var endpoints) || endpoints.ValueKind != JsonValueKind.Object)
                {
                    return null;
                }

                if (!endpoints.TryGetProperty(DockerEndpointKey, out var dockerEndpoint) || dockerEndpoint.ValueKind != JsonValueKind.Object)
                {
                    return null;
                }

                if (dockerEndpoint.TryGetProperty("Host", out var hostElement) && hostElement.ValueKind == JsonValueKind.String)
                {
                    return hostElement.GetString();
                }
            }
            catch (JsonException)
            {
            }
            catch (IOException)
            {
            }

            return null;
        }

        private static string GetDockerConfigDirectory()
        {
            var dockerConfig = Environment.GetEnvironmentVariable("DOCKER_CONFIG");
            if (!string.IsNullOrEmpty(dockerConfig))
            {
                return dockerConfig;
            }

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".docker");
        }

        private static string GetContextsMetaDirectory()
        {
            return Path.Combine(GetDockerConfigDirectory(), "contexts", "meta");
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

        private static Uri GetPlatformDefault()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? new Uri("npipe://./pipe/docker_engine")
                : new Uri("unix:/var/run/docker.sock");
        }
    }
}
