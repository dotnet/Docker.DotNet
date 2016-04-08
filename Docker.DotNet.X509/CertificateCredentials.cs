using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Docker.DotNet.X509
{
    public class CertificateCredentials : Credentials
    {
        private readonly WebRequestHandler _handler;

        public CertificateCredentials(X509Certificate2 clientCertificate)
        {
            _handler = new WebRequestHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                UseDefaultCredentials = false
            };

            _handler.ClientCertificates.Add(clientCertificate);
        }

        public override HttpMessageHandler Handler
        {
            get
            {
                return _handler;
            }
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