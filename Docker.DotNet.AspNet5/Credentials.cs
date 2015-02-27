using System.Net.Http;

namespace Docker.DotNet
{
    public abstract class Credentials
    {
        public abstract HttpClient BuildHttpClient();

        public abstract bool IsTlsCredentials();
    }
}