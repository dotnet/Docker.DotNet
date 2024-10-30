﻿
namespace Docker.DotNet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    internal class NetworkOperations : INetworkOperations
    {
        internal static readonly ApiResponseErrorHandlingDelegate NoSuchNetworkHandler = (statusCode, responseBody) =>
        {
            if (statusCode == HttpStatusCode.NotFound)
            {
                throw new DockerNetworkNotFoundException(statusCode, responseBody);
            }
        };

        private readonly DockerClient _client;

        internal NetworkOperations(DockerClient client)
        {
            this._client = client;
        }

        async Task<IList<NetworkResponse>> INetworkOperations.ListNetworksAsync(NetworksListParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters == null ? null : new QueryString<NetworksListParameters>(parameters);
            return await this._client.MakeRequestAsync<NetworkResponse[]>(this._client.NoErrorHandlers, HttpMethod.Get, "networks", queryParameters, cancellationToken).ConfigureAwait(false);
        }

        async Task<NetworkResponse> INetworkOperations.InspectNetworkAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await this._client.MakeRequestAsync<NetworkResponse>(new[] { NoSuchNetworkHandler }, HttpMethod.Get, $"networks/{id}", cancellationToken).ConfigureAwait(false);
        }

        Task INetworkOperations.DeleteNetworkAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this._client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Delete, $"networks/{id}", cancellationToken);
        }

        async Task<NetworksCreateResponse> INetworkOperations.CreateNetworkAsync(NetworksCreateParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = new JsonRequestContent<NetworksCreateParameters>(parameters, this._client.JsonSerializer);
            return await this._client.MakeRequestAsync<NetworksCreateResponse>(this._client.NoErrorHandlers, HttpMethod.Post, "networks/create", null, data, cancellationToken).ConfigureAwait(false);
        }

        Task INetworkOperations.ConnectNetworkAsync(string id, NetworkConnectParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrEmpty(parameters?.Container))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = new JsonRequestContent<NetworkConnectParameters>(parameters, this._client.JsonSerializer);
            return this._client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Post, $"networks/{id}/connect", null, data, cancellationToken);
        }

        Task INetworkOperations.DisconnectNetworkAsync(string id, NetworkDisconnectParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrEmpty(parameters?.Container))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = new JsonRequestContent<NetworkDisconnectParameters>(parameters, this._client.JsonSerializer);
            return this._client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Post, $"networks/{id}/disconnect", null, data, cancellationToken);
        }

        Task INetworkOperations.DeleteUnusedNetworksAsync(NetworksDeleteUnusedParameters parameters, CancellationToken cancellationToken)
        {
            return ((INetworkOperations)this).PruneNetworksAsync(parameters, cancellationToken);
        }

        async Task<NetworksPruneResponse> INetworkOperations.PruneNetworksAsync(NetworksDeleteUnusedParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters == null ? null : new QueryString<NetworksDeleteUnusedParameters>(parameters);
            return await this._client.MakeRequestAsync<NetworksPruneResponse>(null, HttpMethod.Post, "networks/prune", queryParameters, cancellationToken);
        }
    }
}
