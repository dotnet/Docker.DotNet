using System.Net.Http;
using Org.BouncyCastle.X509;
namespace Docker.DotNet
{
    public class CertificateCredentials : Credentials
    {

        public X509Certificate ClientCertificate { get; private set; }

        public CertificateCredentials(X509Certificate clientCertificate)
        {
            this.ClientCertificate = clientCertificate;
        }

        public override HttpClient BuildHttpClient()
        {
            PortableWebRequestHandler certHandler = new PortableWebRequestHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                UseDefaultCredentials = false
            };
            
            certHandler.ClientCertificates.Add(this.ClientCertificate);

            return new HttpClient(certHandler);
        }
    }
}