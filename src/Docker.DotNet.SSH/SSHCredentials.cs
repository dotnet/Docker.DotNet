using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Net.Http.Client;
using Renci.SshNet;

namespace Docker.DotNet.SSH
{
    public class SSHCredentials : Credentials
    {
        private PrivateKeyFile _privateKey;

        /// <summary>
        /// Creates SSH credentials to handle connecting over SSH.
        /// </summary>
        /// <param name="privateKey">The private key contents</param>
        public SSHCredentials(string privateKey) : this(privateKey, null) { }

        /// <summary>
        /// Creates SSH credentials to handle connecting over SSH.
        /// </summary>
        /// <param name="privateKey">The private key contents</param>
        /// <param name="password">Password for the private key</param>
        public SSHCredentials(string privateKey, string password)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(privateKey);
                writer.Flush();

                stream.Position = 0;
                // This needs to be created *before* the StreamWriter (which closes the stream) is disposed
                _privateKey = new PrivateKeyFile(stream, password);
            }
        }

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

        public override ManagedHandler.StreamOpener GetSshStreamOpener(string user)
        {
            return (string host, int port, CancellationToken cancellationToken) => {
                var authMethod = new PrivateKeyAuthenticationMethod(user, _privateKey);
                var connectionInfo = new ConnectionInfo(host, port, user, authMethod);
                var client = new SshClient(connectionInfo);
                client.Connect();

                var cmd = client.CreateCommand("docker system dial-stdio");
                cmd.BeginExecute();

                var result = new JoinedReadWriteStream(cmd.OutputStream, cmd.InputStream);

                return Task.FromResult((Stream) result);
            };
        }
    }
}
