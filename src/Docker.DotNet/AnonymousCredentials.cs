using System.Net.Http;

namespace Docker.DotNet
{
    public class AnonymousCredentials : Credentials
    {
        public override bool IsTlsCredentials()
        {
            return false;
        }

        public override bool SupportsScheme(string scheme)
        {
            return true;
        }

        public override void Dispose()
        {
        }

        public override HttpMessageHandler GetHandler(HttpMessageHandler innerHandler)
        {
            return innerHandler;
        }
    }
}
