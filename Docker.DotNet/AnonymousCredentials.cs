using System.Net.Http;

namespace Docker.DotNet
{
    public class AnonymousCredentials : Credentials
    {
        private readonly HttpClientHandler _handler;

        public AnonymousCredentials()
        {
            _handler = new HttpClientHandler();
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
            return false;
        }

        public override void Dispose()
        {
            _handler.Dispose();
        }
    }
}