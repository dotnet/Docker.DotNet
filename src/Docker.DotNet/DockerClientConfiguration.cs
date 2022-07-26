using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public DockerClientConfiguration(Credentials credentials = null, TimeSpan defaultTimeout = default)
            : this(GetLocalDockerEndpoint(), credentials, defaultTimeout)
        {
        }

        public DockerClientConfiguration(Uri endpoint, Credentials credentials = null, TimeSpan defaultTimeout = default)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            if (defaultTimeout < Timeout.InfiniteTimeSpan)
            {
                throw new ArgumentException("Default timeout must be greater than -1", nameof(defaultTimeout));
            }

            if (credentials == null)
            {
                credentials = new AnonymousCredentials();
            }

            if (defaultTimeout == default)
            {
                defaultTimeout = TimeSpan.FromSeconds(100);
            }

            EndpointBaseUri = endpoint;
            Credentials = credentials;
            DefaultTimeout = defaultTimeout;
            NamedPipeConnectTimeout = TimeSpan.FromMilliseconds(100);
        }

        public Uri EndpointBaseUri { get; }

        public Credentials Credentials { get; }

        public TimeSpan DefaultTimeout { get; }

        public TimeSpan NamedPipeConnectTimeout { get; set; }

        public DockerClient CreateClient(Version requestedApiVersion = null)
        {
            return new DockerClient(this, requestedApiVersion);
        }

        public void Dispose()
        {
            Credentials.Dispose();
        }

        private static Uri GetLocalDockerEndpoint()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            return isWindows ? new Uri("npipe://./pipe/docker_engine") : new Uri("unix:/var/run/docker.sock");
        }
    }
}