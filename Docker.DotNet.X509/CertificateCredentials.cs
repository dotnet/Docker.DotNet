using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Docker.DotNet.X509
{
    public class CertificateCredentials : Credentials
    {
        private readonly HttpMessageHandler _handler;

        public CertificateCredentials(X509Certificate2 clientCertificate)
        {
            WebRequestHandler certHandler = new WebRequestHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                UseDefaultCredentials = false
            };

            certHandler.ClientCertificates.Add(clientCertificate);

            _handler = certHandler;
        }

        public override HttpClient BuildHttpClient()
        {
            return new HttpClient(_handler, false);
        }

        public override bool IsTlsCredentials()
        {
            return true;
        }

        public override void Dispose()
        {
            _handler.Dispose();
        }
    }
}