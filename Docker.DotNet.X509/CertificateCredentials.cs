using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Docker.DotNet.X509
{
    public class CertificateCredentials : Credentials
    {
        public X509Certificate2 ClientCertificate { get; private set; }

        public CertificateCredentials(X509Certificate2 clientCertificate)
        {
            this.ClientCertificate = clientCertificate;
        }

        public override HttpClient BuildHttpClient()
        {
            WebRequestHandler certHandler = new WebRequestHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                UseDefaultCredentials = false
            };

            certHandler.ClientCertificates.Add(this.ClientCertificate);

            return new HttpClient(certHandler);
        }
    }
}