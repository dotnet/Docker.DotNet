using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    public interface IConfigOperations
    {
        /// <summary>
        /// List configs
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<SwarmConfig>> ListConfigsAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create a configs
        /// </summary>
        /// <remarks>
        /// 201 - No error.
        /// 406 - Server error or node is not part of a swarm.
        /// 409 - Name conflicts with an existing object.
        /// 500 - Server error.
        /// </remarks>
        Task<SwarmCreateConfigResponse> CreateConfigAsync(SwarmCreateConfigParameters body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inspect a configs
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 404 - Secret not found.
        /// 406 - Node is not part of a swarm.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID of the config.</param>
        Task<SwarmConfig> InspectConfigAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove a configs
        /// </summary>
        /// <remarks>
        /// 204 - No error.
        /// 404 - Secret not found.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID of the config.</param>
        Task RemoveConfigAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
    }
}