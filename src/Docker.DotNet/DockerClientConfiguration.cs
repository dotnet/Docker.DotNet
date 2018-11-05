using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public Uri EndpointBaseUri { get; internal set; }

        public Credentials Credentials { get; internal set; }

        public TimeSpan DefaultTimeout { get; internal set; } = TimeSpan.FromSeconds(100);

        // NamedPipeClientStream handles file not found by polling until the server arrives. Use a short
        // timeout so that the user doesn't get stuck waiting for a dockerd instance that is not running.
        public TimeSpan NamedPipeConnectTimeout { get; set; } = TimeSpan.FromMilliseconds(100);

        private static Uri LocalDockerUri()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            return isWindows ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:/var/run/docker.sock");
        }

        public DockerClientConfiguration(Credentials credentials = null, TimeSpan defaultTimeout = default(TimeSpan))
            : this(LocalDockerUri(), credentials, defaultTimeout)
        {
        }

        public DockerClientConfiguration(Uri endpoint, Credentials credentials = null,
            TimeSpan defaultTimeout = default(TimeSpan))
        {
            if (endpoint == null)
                throw new ArgumentNullException(nameof(endpoint));


            Credentials = credentials ?? new AnonymousCredentials();
            EndpointBaseUri = endpoint;
            if (defaultTimeout != TimeSpan.Zero)
            {
                if (defaultTimeout < Timeout.InfiniteTimeSpan)
                    // TODO: Should be a resource for localization.
                    // TODO: Is this a good message?
                    throw new ArgumentException("Timeout must be greater than Timeout.Infinite", nameof(defaultTimeout));
                DefaultTimeout = defaultTimeout;
            }
        }

        public DockerClient CreateClient()
        {
            return this.CreateClient(null);
        }

        public DockerClient CreateClient(Version requestedApiVersion)
        {
            return new DockerClient(this, requestedApiVersion);
        }

        public void Dispose()
        {
            Credentials.Dispose();
        }
    }
}