
namespace Docker.DotNet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
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

        public Task ConnectNetworkAsync(string id, NetworkConnectParameters parameters)
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
            return this._client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Get, $"networks/{id}/connect", null, data);
        }

        public async Task<NetworksCreateResponse> CreateNetworkAsync(NetworksCreateParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = new JsonRequestContent<NetworksCreateParameters>(parameters, this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Post, "networks/create", null, data).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<NetworksCreateResponse>(response.Body);
        }

        public Task DeleteNetworkAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this._client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Delete, $"networks/{id}", null);
        }

        public Task DisconnectNetworkAsync(string id, NetworkDisconnectParameters parameters)
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
            return this._client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Get, $"networks/{id}/disconnect", null, data);
        }

        public async Task<NetworkResponse> InspectNetworkAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await this._client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Get, $"networks/{id}", null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<NetworkResponse>(response.Body);
        }

        public async Task<IList<NetworkListResponse>> ListNetworksAsync(NetworksListParameters parameters)
        {
            var queryParameters = parameters == null ? null : new QueryString<NetworksListParameters>(parameters);
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "networks", queryParameters).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<NetworkListResponse[]>(response.Body);
        }
    }
}
