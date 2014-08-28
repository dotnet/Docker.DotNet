using System;

namespace Docker.DotNet
{
    public class DockerClientConfiguration
    {
        public Uri EndpointBaseUri { get; private set; }

        public Credentials Credentials { get; private set; }

        public DockerClientConfiguration(Uri endpoint) : this(endpoint, new AnonymousCredentials())
        {
        }

        public DockerClientConfiguration(Uri endpoint, Credentials credentials)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }

            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }

            this.EndpointBaseUri = endpoint;
            this.Credentials = credentials;
        }

        public DockerClient CreateClient()
        {
            return new DockerClient(this);
        }
    }
}