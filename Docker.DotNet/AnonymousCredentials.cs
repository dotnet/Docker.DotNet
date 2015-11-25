using System.Net.Http;

namespace Docker.DotNet
{
    public class AnonymousCredentials : Credentials
    {
        private readonly HttpMessageHandler _handler;

        public AnonymousCredentials()
        {
            _handler = new HttpClientHandler();
        }

        public override HttpClient BuildHttpClient()
        {
            return new HttpClient(_handler, false);
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