using System;

namespace Docker.DotNet.X509
{
    public class DockerCertificateClientConfiguration : DockerClientConfiguration
    {
        public DockerCertificateClientConfiguration(Uri endpoint, Credentials credentials) :base(endpoint, credentials)
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

       
    }
}