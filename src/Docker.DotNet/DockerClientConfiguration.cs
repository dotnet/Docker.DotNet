using System;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public Uri EndpointBaseUri { get; internal set; }

        public Credentials Credentials { get; internal set; }

        public TimeSpan DefaultTimeout { get; internal set; } = TimeSpan.FromSeconds(100);

        public DockerClientConfiguration(Uri endpoint, TimeSpan defaultTimeout) : this(endpoint, null, defaultTimeout) { }

        public DockerClientConfiguration(Uri endpoint) : this(endpoint, null) { }

        public DockerClientConfiguration(Uri endpoint, Credentials credentials) : this(endpoint, credentials, TimeSpan.FromSeconds(100)) { }

        public DockerClientConfiguration(Uri endpoint, Credentials credentials, TimeSpan defaultTimeout)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Credentials = credentials ?? new AnonymousCredentials();
            EndpointBaseUri = endpoint;
            DefaultTimeout = defaultTimeout;
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