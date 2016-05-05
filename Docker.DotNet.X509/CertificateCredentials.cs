using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Net.Http.Client;

namespace Docker.DotNet.X509
{
    public class CertificateCredentials : Credentials
    {
        private X509Certificate2 _certificate;

        public CertificateCredentials(X509Certificate2 clientCertificate)
        {
            _certificate = clientCertificate;
        }

        public override HttpMessageHandler GetHandler(HttpMessageHandler innerHandler)
        {
            var handler = (ManagedHandler)innerHandler;
            handler.ClientCertificates = new X509CertificateCollection
            {
                _certificate
            };

            handler.ServerCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
            return handler;
        }

        public override bool IsTlsCredentials()
        {
            return true;
        }

        public override void Dispose()
        {
        }
    }
}