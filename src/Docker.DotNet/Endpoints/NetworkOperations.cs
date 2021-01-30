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
            _client = client;
        }

        Task INetworkOperations.ConnectNetworkAsync(string id, NetworkConnectParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrEmpty(parameters?.Container))
            {
                throw new ArgumentNullException($"{nameof(parameters)}.{nameof(parameters.Container)}");
            }

            var data = new JsonRequestContent<NetworkConnectParameters>(parameters, _client.JsonSerializer);
            return _client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Post, $"networks/{id}/connect", null, data, cancellationToken);
        }

        async Task<NetworksCreateResponse> INetworkOperations.CreateNetworkAsync(NetworksCreateParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = new JsonRequestContent<NetworksCreateParameters>(parameters, _client.JsonSerializer);
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Post, "networks/create", null, data, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<NetworksCreateResponse>(response.Body);
        }

        Task INetworkOperations.DeleteNetworkAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Delete, $"networks/{id}", cancellationToken);
        }

        Task INetworkOperations.DeleteUnusedNetworksAsync(NetworksDeleteUnusedParameters parameters, CancellationToken cancellationToken)
        {
            return ((INetworkOperations)this).PruneNetworksAsync(parameters, cancellationToken);
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

            var data = new JsonRequestContent<NetworkDisconnectParameters>(parameters, _client.JsonSerializer);
            return _client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Post, $"networks/{id}/disconnect", null, data, cancellationToken);
        }

        async Task<NetworkResponse> INetworkOperations.InspectNetworkAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await _client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Get, $"networks/{id}", cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<NetworkResponse>(response.Body);
        }

        async Task<IList<NetworkResponse>> INetworkOperations.ListNetworksAsync(NetworksListParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters == null ? null : new QueryString<NetworksListParameters>(parameters);
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Get, "networks", queryParameters, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<NetworkResponse[]>(response.Body);
        }

        async Task<NetworksPruneResponse> INetworkOperations.PruneNetworksAsync(NetworksDeleteUnusedParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters == null ? null : new QueryString<NetworksDeleteUnusedParameters>(parameters);
            var response = await _client.MakeRequestAsync(null, HttpMethod.Post, "networks/prune", queryParameters, cancellationToken);
            return _client.JsonSerializer.DeserializeObject<NetworksPruneResponse>(response.Body);
        }
    }
}
