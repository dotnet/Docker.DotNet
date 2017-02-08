using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    public interface ISwarmOperations
    {
        #region Swarm

        /// <summary>
        /// Get the unlock key.
        /// </summary>
        /// <remarks>
        /// 200 - No Error.
        /// 500 - Server Error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        Task<SwarmUnlockResponse> GetSwarmUnlockKeyAsync();

        /// <summary>
        /// Initialize a new swarm.
        /// </summary>
        /// <remarks>
        /// 200 - No Error.
        /// 400 - Bad parameters.
        /// 500 - Server Error.
        /// 503 - Node is already part of a swarm.
        /// </remarks>
        /// <param name="parameters">The join parameters.</param>
        /// <returns>The node id.</returns>
        Task<string> InitSwarmAsync(SwarmInitParameters parameters);

        /// <summary>
        /// Inspect swarm.
        /// </summary>
        /// <remarks>
        /// 200 - No Error.
        /// 404 - No such swarm.
        /// 500 - Server Error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        Task<ClusterInfo> InspectSwarmAsync();

        /// <summary>
        /// Join an existing swarm.
        /// </summary>
        /// <remarks>
        /// 200 - No Error.
        /// 500 - Server Error.
        /// 503 - Node is already part of a swarm.
        /// </remarks>
        /// <param name="parameters">The join parameters.</param>
        Task JoinSwarmAsync(SwarmJoinParameters parameters);

        /// <summary>
        /// Leave a swarm.
        /// </summary>
        /// <remarks>
        /// 200 - No Error.
        /// 500 - Server Error.
        /// 503 - Node not part of a swarm.
        /// </remarks>
        /// <param name="parameters">The leave parameters.</param>
        Task LeaveSwarmAsync(SwarmLeaveParameters parameters = null);

        /// <summary>
        /// Unlock a locked manager.
        /// </summary>
        /// <remarks>
        /// 200 - No Error.
        /// 500 - Server Error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        /// <param name="parameters">The swarm's unlock key.</param>
        Task UnlockSwarmAsync(SwarmUnlockParameters parameters);

        /// <summary>
        /// Update a swarm.
        /// </summary>
        /// <remarks>
        /// 200 - No Error.
        /// 400 - Bad parameter.
        /// 500 - Server Error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        /// <param name="parameters">The update parameters.</param>
        Task UpdateSwarmAsync(SwarmUpdateParameters parameters);

        #endregion Swarm

        #region Services

        /// <summary>
        /// Create a service.
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 400 - Bad parameter.
        /// 403 - Network is not eligible for services.
        /// 409 - Name conflicts with an existing service.
        /// 500 - Server error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        Task<ServiceCreateResponse> CreateServiceAsync(ServiceCreateParameters parameters);

        /// <summary>
        /// Inspect a service.
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 404 - No such service.
        /// 500 - Server error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        /// <param name="id">ID or name of service.</param>
        /// <returns>The service spec.</returns>
        Task<SwarmService> InspectServiceAsync(string id);

        /// <summary>
        /// List services.
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 500 - Server error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        Task<IEnumerable<SwarmService>> ListServicesAsync();

        /// <summary>
        /// Update a service.
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 400 - Bad parameter.
        /// 404 - No such service.
        /// 500 - Server error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        /// <param name="id">ID or name of service.</param>
        /// <returns>The service spec.</returns>
        Task<ServiceUpdateResponse> UpdateServiceAsync(string id, ServiceUpdateParameters parameters);

        /// <summary>
        /// Delete a service.
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 404 - No such service.
        /// 500 - Server error.
        /// 503 - Node is not part of a swarm.
        /// </remarks>
        /// <param name="id">ID or name of service.</param>
        Task RemoveServiceAsync(string id);

        #endregion Services
    }
}