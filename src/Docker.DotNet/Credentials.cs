using System;
using System.Net.Http;
using Microsoft.Net.Http.Client;

namespace Docker.DotNet
{
    public abstract class Credentials : IDisposable
    {
        public abstract bool IsTlsCredentials();

        public abstract bool IsSshCredentials();

        public abstract HttpMessageHandler GetHandler(HttpMessageHandler innerHandler);

        public virtual ManagedHandler.StreamOpener GetStreamOpener()
        {
            return null;
        }

        public virtual void Dispose()
        {
        }
    }
}
