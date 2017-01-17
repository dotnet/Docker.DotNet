
namespace Docker.DotNet
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface INetworkOperations
    {
        Task ConnectNetworkAsync(string id, NetworkConnectParameters parameters);

        Task<NetworksCreateResponse> CreateNetworkAsync(NetworksCreateParameters parameters);

        Task DeleteNetworkAsync(string id);

        Task DisconnectNetworkAsync(string id, NetworkDisconnectParameters parameters);

        Task<NetworkResponse> InspectNetworkAsync(string id);

        Task<IList<NetworkListResponse>> ListNetworksAsync(NetworksListParameters parameters = null);
    }
}
