using System.Net.Http;

namespace Docker.DotNet
{
    public class AnonymousCredentials : Credentials
    {
        public AnonymousCredentials()
        {
        }

        public override HttpClient BuildHttpClient()
        {
            return new HttpClient();
        }
    }
}