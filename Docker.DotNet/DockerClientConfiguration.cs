using System;

namespace Docker.DotNet
{
    public class DockerClientConfiguration
    {
        public Uri EndpointBaseUri { get; private set; }

        public Credentials Credentials { get; private set; }

        public DockerClientConfiguration(Uri endpoint)
            : this(endpoint, new AnonymousCredentials())
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

            bool isTls = credentials is CertificateCredentials;
            this.EndpointBaseUri = SanitizeEndpoint(endpoint, isTls);
            this.Credentials = credentials;
        }

        public DockerClient CreateClient()
        {
            return this.CreateClient(null);
        }

        public DockerClient CreateClient(Version requestedApiVersion)
        {
            return new DockerClient(this, requestedApiVersion);
        }

        private static Uri SanitizeEndpoint(Uri endpoint, bool isTls)
        {
            UriBuilder builder = new UriBuilder(endpoint);

            if (isTls)
            {
                builder.Scheme = "https";
            }
            else if (builder.Scheme.Equals("tcp", StringComparison.InvariantCultureIgnoreCase))
            {
                builder.Scheme = "http";
            }

            return builder.Uri;
        }
    }
}