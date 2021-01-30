using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public DockerClientConfiguration(Credentials credentials = null, TimeSpan defaultTimeout = default)
            : this(LocalDockerUri(), credentials, defaultTimeout)
        {
        }

        public DockerClientConfiguration(Uri endpoint, Credentials credentials = null,
            TimeSpan defaultTimeout = default)
        {
            if (endpoint == null)
                throw new ArgumentNullException(nameof(endpoint));

            Credentials = credentials ?? new AnonymousCredentials();
            EndpointBaseUri = endpoint;
            if (defaultTimeout != TimeSpan.Zero)
            {
                if (defaultTimeout < Timeout.InfiniteTimeSpan)
                {
                    // TODO: Should be a resource for localization.
                    // TODO: Is this a good message?
                    throw new ArgumentException("Timeout must be greater than Timeout.Infinite", nameof(defaultTimeout));
                }
                DefaultTimeout = defaultTimeout;
            }
        }

        public Credentials Credentials { get; internal set; }
        public TimeSpan DefaultTimeout { get; internal set; } = TimeSpan.FromSeconds(100);
        public Uri EndpointBaseUri { get; internal set; }
        public TimeSpan NamedPipeConnectTimeout { get; set; } = TimeSpan.FromMilliseconds(100);

        public DockerClient CreateClient()
        {
            return CreateClient(null);
        }

        public DockerClient CreateClient(Version requestedApiVersion)
        {
            return new DockerClient(this, requestedApiVersion);
        }

        public void Dispose()
        {
            Credentials.Dispose();
        }

        private static Uri LocalDockerUri()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            return isWindows ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:/var/run/docker.sock");
        }
    }
}
