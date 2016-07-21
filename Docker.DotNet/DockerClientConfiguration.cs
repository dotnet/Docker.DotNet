using System;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public Uri EndpointBaseUri { get; internal set; }

        public Credentials Credentials { get; internal set; }

        public DockerClientConfiguration(Uri endpoint)
            : this(endpoint, null)
        {
        }

        public DockerClientConfiguration(Uri endpoint, Credentials credentials)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Credentials = credentials ?? new AnonymousCredentials();
            EndpointBaseUri = endpoint;
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