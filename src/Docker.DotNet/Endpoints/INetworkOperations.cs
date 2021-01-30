namespace Docker.DotNet
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface INetworkOperations
    {
        /// <summary>
        /// Connect a container to a network.
        /// </summary>
        /// <remarks>
        /// docker network connect
        ///
        /// 200 - No error.
        /// 403 - Operation not supported for swarm scoped networks.
        /// 404 - Network or container not found.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">Network ID or name.</param>
        Task ConnectNetworkAsync(string id, NetworkConnectParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a network.
        /// </summary>
        /// <remarks>
        /// docker network create
        ///
        /// 201 - No error.
        /// 403 - Operation not supported for pre-defined networks.
        /// 404 - Plugin not found.
        /// 500 - Server error.
        /// </remarks>
        Task<NetworksCreateResponse> CreateNetworkAsync(NetworksCreateParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove a network.
        /// </summary>
        /// <remarks>
        /// docker network rm
        ///
        /// 204 - No error.
        /// 404 - No such network.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">Network ID or name.</param>
        Task DeleteNetworkAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete unused networks.
        /// </summary>
        /// <param name="id">Network ID or name.</param>
        /// <remarks>
        /// docker network disconnect
        ///
        /// HTTP POST /networks/prune
        ///
        /// 200 - No error.
        /// 403 - Operation not supported for swarm scoped networks.
        /// 404 - Network or container not found.
        /// 500 - Server error.
        /// </remarks>
        [System.Obsolete("Use INetworkOperations.PruneNetworksAsync")]
        Task DeleteUnusedNetworksAsync(NetworksDeleteUnusedParameters parameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Disconnect a container from a network.
        /// </summary>
        /// <param name="id">Network ID or name.</param>
        /// <remarks>
        /// docker network disconnect
        ///
        /// 200 - No error.
        /// 403 - Operation not supported for swarm scoped networks.
        /// 404 - Network or container not found.
        /// 500 - Server error.
        /// </remarks>
        Task DisconnectNetworkAsync(string id, NetworkDisconnectParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inspect a network.
        /// </summary>
        /// <remarks>
        /// docker network inspect
        ///
        /// 200 - No error.
        /// 404 - Network not found.
        /// </remarks>
        /// <param name="id">Network ID or name.</param>
        Task<NetworkResponse> InspectNetworkAsync(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// List networks.
        /// </summary>
        /// <remarks>
        /// docker network ls
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<NetworkResponse>> ListNetworksAsync(NetworksListParameters parameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete unused networks.
        /// </summary>
        /// <param name="id">Network ID or name.</param>
        /// <remarks>
        /// docker network disconnect
        ///
        /// HTTP POST /networks/prune
        ///
        /// 200 - No error.
        /// 403 - Operation not supported for swarm scoped networks.
        /// 404 - Network or container not found.
        /// 500 - Server error.
        /// </remarks>
        Task<NetworksPruneResponse> PruneNetworksAsync(NetworksDeleteUnusedParameters parameters = null, CancellationToken cancellationToken = default);
    }
}
