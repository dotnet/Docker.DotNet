using System.IO;
using System.Net.Http;
using System.Threading;
using Microsoft.Net.Http.Client;

namespace Docker.DotNet.SSH
{
    public class SSHCredentials : Credentials
    {
        public override HttpMessageHandler GetHandler(HttpMessageHandler innerHandler)
        {
            return innerHandler;
        }

        public override bool IsSshCredentials()
        {
            return true;
        }

        public override bool IsTlsCredentials()
        {
            return false;
        }

        public override ManagedHandler.StreamOpener GetStreamOpener()
        {
            // TODO
            return async (string host, int port, CancellationToken cancellationToken) => {
                return File.OpenRead("/dev/null");
            };
        }
    }
}
