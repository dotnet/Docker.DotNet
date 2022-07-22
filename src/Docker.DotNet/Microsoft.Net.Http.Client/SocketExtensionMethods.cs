using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Net.Http.Client
{
    public static class SocketExtensionMethods
    {
        public static async Task ConnectAsync(this Socket socket, IPAddress address, int port, CancellationToken cancellationToken)
        {
            Task socketConnectTask = socket.ConnectAsync(address, port);
            Task delayTask = Task.Delay(int.MaxValue, cancellationToken);

            await await Task.WhenAny(socketConnectTask, delayTask);
        }
    }
}
