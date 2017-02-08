
namespace Docker.DotNet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    internal class SwarmOperations : ISwarmOperations
    {
        internal static readonly ApiResponseErrorHandlingDelegate SwarmResponseHandler = (statusCode, responseBody) =>
        {
            if (statusCode == HttpStatusCode.ServiceUnavailable)
            {
                // TODO: Make typed error.
                throw new Exception("Node is not part of a swarm.");
            }
        };

        private readonly DockerClient _client;

        internal SwarmOperations(DockerClient client)
        {
            this._client = client;
        }

        async Task<ServiceCreateResponse> ISwarmOperations.CreateServiceAsync(ServiceCreateParameters parameters)
        {
            var data = new JsonRequestContent<ServiceCreateParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Post, "services/create", null, RegistryAuthHeaders(parameters.RegistryAuth), data).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ServiceCreateResponse>(response.Body);
        }

        async Task<SwarmUnlockResponse> ISwarmOperations.GetSwarmUnlockKeyAsync()
        {
            var response = await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Get, "swarm/unlockkey", null, null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmUnlockResponse>(response.Body);
        }

        async Task<string> ISwarmOperations.InitSwarmAsync(SwarmInitParameters parameters)
        {
            var data = new JsonRequestContent<SwarmInitParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/init", null, data).ConfigureAwait(false);
            return response.Body;
        }

        async Task<SwarmService> ISwarmOperations.InspectServiceAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Get, $"services/{id}", null, null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmService>(response.Body);
        }

        async Task<ClusterInfo> ISwarmOperations.InspectSwarmAsync()
        {
            var response = await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Get, "swarm", null, null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ClusterInfo>(response.Body);
        }

        async Task ISwarmOperations.JoinSwarmAsync(SwarmJoinParameters parameters)
        {
            var data = new JsonRequestContent<SwarmJoinParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), this._client.JsonSerializer);
            await this._client.MakeRequestAsync(
                new ApiResponseErrorHandlingDelegate[]
                {
                    (statusCode, responseBody) =>
                    {
                        if (statusCode == HttpStatusCode.ServiceUnavailable)
                        {
                            // TODO: Make typed error.
                            throw new Exception("Node is already part of a swarm.");
                        }
                    }
                },
                HttpMethod.Post,
                "swarm/join",
                null,
                data).ConfigureAwait(false);
        }

        async Task ISwarmOperations.LeaveSwarmAsync(SwarmLeaveParameters parameters)
        {
            var query = parameters == null ? null : new QueryString<SwarmLeaveParameters>(parameters);
            await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/leave", query, null).ConfigureAwait(false);
        }

        async Task<IEnumerable<SwarmService>> ISwarmOperations.ListServicesAsync()
        {
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Get, $"services", null, null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmService[]>(response.Body);
        }

        async Task ISwarmOperations.RemoveServiceAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Delete, $"services/{id}", null, null).ConfigureAwait(false);
        }

        async Task ISwarmOperations.UnlockSwarmAsync(SwarmUnlockParameters parameters)
        {
            var body = new JsonRequestContent<SwarmUnlockParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), this._client.JsonSerializer);
            await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/unlock", null, body).ConfigureAwait(false);
        }

        async Task<ServiceUpdateResponse> ISwarmOperations.UpdateServiceAsync(string id, ServiceUpdateParameters parameters)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var query = new QueryString<ServiceUpdateParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            var body = new JsonRequestContent<ServiceUpdateParameters>(parameters, this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Post, $"services/{id}", query, RegistryAuthHeaders(parameters.RegistryAuth), body).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ServiceUpdateResponse>(response.Body);
        }

        async Task ISwarmOperations.UpdateSwarmAsync(SwarmUpdateParameters parameters)
        {
            var query = new QueryString<SwarmUpdateParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            var body = new JsonRequestContent<Spec>(parameters.Spec ?? throw new ArgumentNullException(nameof(parameters.Spec)), this._client.JsonSerializer);
            await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/update", query, body).ConfigureAwait(false);
        }

        private IDictionary<string, string> RegistryAuthHeaders(AuthConfig authConfig)
        {
            if (authConfig == null)
            {
                return new Dictionary<string, string>();
            }

            return new Dictionary<string, string>
            {
                {
                    "X-Registry-Auth",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(this._client.JsonSerializer.SerializeObject(authConfig)))
                }
            };
        }
    }
}