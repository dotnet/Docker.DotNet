
namespace Docker.DotNet
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using System.Threading;

    public interface INetworkOperations
    {
        Task ConnectNetworkAsync(string id, NetworkConnectParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<NetworksCreateResponse> CreateNetworkAsync(NetworksCreateParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteNetworkAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task DisconnectNetworkAsync(string id, NetworkDisconnectParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<NetworkResponse> InspectNetworkAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<NetworkListResponse>> ListNetworksAsync(NetworksListParameters parameters = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
