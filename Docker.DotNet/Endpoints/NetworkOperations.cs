
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

        private DockerClient Client { get; set; }

        internal NetworkOperations(DockerClient client)
        {
            this.Client = client;
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

            var path = $"networks/{id}/connect";
            var data = new JsonRequestContent<NetworkConnectParameters>(parameters, this.Client.JsonSerializer);
            return this.Client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Get, path, null, data);
        }

        public async Task<NetworksCreateResponse> CreateNetworkAsync(NetworksCreateParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var path = "networks/create";
            var data = new JsonRequestContent<NetworksCreateParameters>(parameters, this.Client.JsonSerializer);
            var response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, null, data).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<NetworksCreateResponse>(response.Body);
        }

        public Task DeleteNetworkAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"networks/{id}";
            return this.Client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Delete, path, null);
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

            var path = $"networks/{id}/disconnect";
            var data = new JsonRequestContent<NetworkDisconnectParameters>(parameters, this.Client.JsonSerializer);
            return this.Client.MakeRequestAsync(new[] { NoSuchNetworkHandler }, HttpMethod.Get, path, null, data);
        }

        public async Task<NetworkResponse> InspectNetworkAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"networks/{id}/json";
            var response = await this.Client.MakeRequestAsync(new[] {NoSuchNetworkHandler}, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<NetworkResponse>(response.Body);
        }

        public async Task<IList<NetworkListResponse>> ListNetworksAsync(NetworksListParameters parameters)
        {
            var path = "networks/json";
            var queryParameters = parameters == null ? null : new QueryString<NetworksListParameters>(parameters);
            var response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<NetworkListResponse[]>(response.Body);
        }
    }
}
