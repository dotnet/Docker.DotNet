using System;

namespace Docker.DotNet
{
    public class DockerClientConfiguration : IDisposable
    {
        public Uri EndpointBaseUri { get; internal set; }

        public Credentials Credentials { get; internal set; }

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

            EndpointBaseUri = SanitizeEndpoint(endpoint, credentials.IsTlsCredentials());
            Credentials = credentials;
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
            else if (builder.Scheme.Equals("tcp", StringComparison.CurrentCultureIgnoreCase)) // JMG: Changed from InvariantCultureIgnoreCase, not supported in PCL
            {
                builder.Scheme = "http";
            }

            return builder.Uri;
        }

        public void Dispose()
        {
            Credentials.Dispose();
        }
    }
}