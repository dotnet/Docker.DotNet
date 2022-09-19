using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class ConfigsOperations : IConfigsOperations
    {
        private readonly DockerClient _client;

        internal ConfigsOperations(DockerClient client)
        {
            this._client = client;
        }

        async Task<IList<SwarmConfig>> IConfigsOperations.ListAsync(CancellationToken cancellationToken)
        {
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "configs", cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<IList<SwarmConfig>>(response.Body);
        }

        async Task<SwarmCreateConfigResponse> IConfigsOperations.CreateAsync(SwarmCreateConfigParameters body, CancellationToken cancellationToken)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            var data = new JsonRequestContent<SwarmCreateConfigParameters>(body, this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Post, "configs/create", null, data, cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmCreateConfigResponse>(response.Body);
        }

        async Task<SwarmConfig> IConfigsOperations.InspectAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, $"configs/{id}", cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmConfig>(response.Body);
        }

        Task IConfigsOperations.DeleteAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Delete, $"configs/{id}", cancellationToken);
        }
    }
}