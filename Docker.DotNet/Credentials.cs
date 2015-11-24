using System;
using System.Net.Http;

namespace Docker.DotNet
{
    public abstract class Credentials : IDisposable
    {
        public abstract HttpClient BuildHttpClient();

        public abstract bool IsTlsCredentials();

        public virtual void Dispose()
        {
        }
    }
}