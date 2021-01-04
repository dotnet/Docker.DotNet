namespace Docker.DotNet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using Models;
    using System.IO;

    internal class SwarmOperations : ISwarmOperations
    {
        internal readonly ApiResponseErrorHandlingDelegate SwarmResponseHandler;

        private readonly DockerClient _client;

        internal SwarmOperations(DockerClient client)
        {
            this._client = client;
            SwarmResponseHandler = new ApiResponseErrorHandlingDelegate(ErrorHandler);
        }

		async Task<ServiceCreateResponse> ISwarmOperations.CreateServiceAsync(ServiceCreateParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var data = new JsonRequestContent<ServiceSpec>(parameters.Service ?? throw new ArgumentNullException(nameof(parameters.Service)), this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Post, "services/create", null, data, RegistryAuthHeaders(parameters.RegistryAuth), cancellationToken).ConfigureAwait(false);

            return this._client.JsonSerializer.DeserializeObject<ServiceCreateResponse>(response.Body);
        }
		
		async Task<SwarmUnlockResponse> ISwarmOperations.GetSwarmUnlockKeyAsync(CancellationToken cancellationToken)
        {
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler },HttpMethod.Get,"swarm/unlockkey",cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmUnlockResponse>(response.Body);
        }
		
		async Task<string> ISwarmOperations.InitSwarmAsync(SwarmInitParameters parameters, CancellationToken cancellationToken)
        {
            var data = new JsonRequestContent<SwarmInitParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), this._client.JsonSerializer);

            var response = await _client.MakeRequestAsync(
                new[] { SwarmResponseHandler },
                HttpMethod.Post,
                "swarm/init",
                null,
                data,
                cancellationToken).ConfigureAwait(false);

            return response.Body;
        }
		
		 async Task<SwarmService> ISwarmOperations.InspectServiceAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler },HttpMethod.Get,$"services/{id}",cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmService>(response.Body);
        }
		
		async Task<SwarmInspectResponse> ISwarmOperations.InspectSwarmAsync(CancellationToken cancellationToken)
        {
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler },HttpMethod.Get,"swarm",cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmInspectResponse>(response.Body);
        }
				
        async Task<DockerApiResponse> ISwarmOperations.JoinSwarmAsync(SwarmJoinParameters parameters, CancellationToken cancellationToken)
        {
            var data = new JsonRequestContent<SwarmJoinParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), _client.JsonSerializer);

            var response = await _client.MakeRequestAsync(
                new[] { SwarmResponseHandler },
                HttpMethod.Post,
                "swarm/join",
                null,
                data,
                cancellationToken).ConfigureAwait(false);

            return response;
        }



        async Task<DockerApiResponse> ISwarmOperations.LeaveSwarmAsync(SwarmLeaveParameters parameters, CancellationToken cancellationToken)
        {
            var query = parameters is null ? null : new QueryString<SwarmLeaveParameters>(parameters);

            var response = await _client.MakeRequestAsync(
                new[] { SwarmResponseHandler },
                HttpMethod.Post,
                "swarm/leave",
                query,
                cancellationToken).ConfigureAwait(false);

            return response;
        }
		
		 async Task<IEnumerable<SwarmService>> ISwarmOperations.ListServicesAsync(ServicesListParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters != null ? new QueryString<ServicesListParameters>(parameters) : null;

            var response = await this._client
			.MakeRequestAsync( new[] { SwarmResponseHandler }, HttpMethod.Get, $"services", queryParameters, cancellationToken)
			.ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmService[]>(response.Body);
        }

		async Task<DockerApiResponse> ISwarmOperations.RemoveServiceAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            return await this._client.MakeRequestAsync(new[] { SwarmResponseHandler },HttpMethod.Delete,$"services/{id}",cancellationToken).ConfigureAwait(false);
        }
		
		async Task<DockerApiResponse> ISwarmOperations.UnlockSwarmAsync(SwarmUnlockParameters parameters, CancellationToken cancellationToken)
        {
            var body = new JsonRequestContent<SwarmUnlockParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), this._client.JsonSerializer);
            return await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Post, "swarm/unlock", null, body, cancellationToken).ConfigureAwait(false);        }
		
		async Task<ServiceUpdateResponse> ISwarmOperations.UpdateServiceAsync(string id, ServiceUpdateParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var query = new QueryString<ServiceUpdateParameters>(parameters);
            var body = new JsonRequestContent<ServiceSpec>(parameters.Service ?? throw new ArgumentNullException(nameof(parameters.Service)), _client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Post, $"services/{id}/update", query, body, RegistryAuthHeaders(parameters.RegistryAuth), cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ServiceUpdateResponse>(response.Body);
        }
		
		 async Task<DockerApiResponse> ISwarmOperations.UpdateSwarmAsync(SwarmUpdateParameters parameters, CancellationToken cancellationToken)
        {
            var query = new QueryString<SwarmUpdateParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            var body = new JsonRequestContent<Spec>(parameters.Spec ?? throw new ArgumentNullException(nameof(parameters.Spec)), _client.JsonSerializer);

            return await _client.MakeRequestAsync(
                new[] { SwarmResponseHandler },
                HttpMethod.Post,
                "swarm/update",
                query,
                body,
                cancellationToken).ConfigureAwait(false);
        }


			
        async Task<SwarmCreateConfigResponse> ISwarmOperations.CreateConfigAsync(SwarmCreateConfigParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters is null) throw new ArgumentNullException(nameof(parameters));
            if (parameters.Config is null) throw new ArgumentNullException(nameof(parameters.Config));

            var data = new JsonRequestContent<ConfigSpec>(parameters.Config, _client.JsonSerializer);
            var response = await _client
                .MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Post, "configs/create", null, data, cancellationToken)
                .ConfigureAwait(false);

            return _client.JsonSerializer.DeserializeObject<SwarmCreateConfigResponse>(response.Body);
        }

        

        public Task<Stream> GetServiceLogsAsync(string id, ServiceLogsParameters parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (parameters is null) throw new ArgumentNullException(nameof(parameters));

            IQueryString queryParameters = new QueryString<ServiceLogsParameters>(parameters);
            return _client.MakeRequestForStreamAsync(new[] { SwarmResponseHandler }, HttpMethod.Get, $"services/{id}/logs", queryParameters, cancellationToken);
        }

        public async Task<MultiplexedStream> GetServiceLogsAsync(string id, bool tty, ServiceLogsParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters is null) throw new ArgumentNullException(nameof(parameters));
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            IQueryString queryParameters = new QueryString<ServiceLogsParameters>(parameters);
            Stream result = await _client.MakeRequestForStreamAsync(new[] { SwarmResponseHandler }, HttpMethod.Get, $"services/{id}/logs", queryParameters, cancellationToken).ConfigureAwait(false);

            return new MultiplexedStream(result, !tty);
        }

       

        

        async Task<SwarmConfig> ISwarmOperations.InspectConfigAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var response = await _client.MakeRequestAsync(
                new[] { SwarmResponseHandler },
                HttpMethod.Get,
                $"configs/{id}",
                cancellationToken)
                .ConfigureAwait(false);

            return _client.JsonSerializer.DeserializeObject<SwarmConfig>(response.Body);
        }

        
       

        

        async Task<IEnumerable<SwarmConfig>> ISwarmOperations.ListConfigsAsync(ConfigsListParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters is null ? null : new QueryString<ConfigsListParameters>(parameters);

            var response = await _client
                .MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Get, $"configs", queryParameters, cancellationToken)
                .ConfigureAwait(false);

            return _client.JsonSerializer.DeserializeObject<SwarmConfig[]>(response.Body);
        }


       

       
				
        async Task<DockerApiResponse> ISwarmOperations.RemoveConfigAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            return await _client.MakeRequestAsync(
                new[] { SwarmResponseHandler },
                HttpMethod.Delete,
                $"configs/{id}",
                cancellationToken).ConfigureAwait(false);
        }

        


       
        



        async Task<DockerApiResponse> ISwarmOperations.UpdateConfigAsync(string id, SwarmUpdateConfigParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var query = new QueryString<SwarmUpdateConfigParameters>(parameters);
            var body = new JsonRequestContent<ConfigSpec>(parameters.Config ?? throw new ArgumentNullException(nameof(parameters.Config)), _client.JsonSerializer);

            return await _client.MakeRequestAsync(
                new[] { SwarmResponseHandler },
                HttpMethod.Post,
                $"configs/{id}/update",
                query,
                body,
                cancellationToken).ConfigureAwait(false);
        }

       

        
		


        internal void ErrorHandler(HttpStatusCode statusCode, string responseBody)
        {
            if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest)
            {
                var deserializedBody = _client.JsonSerializer.DeserializeObject<JSONError>(responseBody);
                throw new DockerSwarmException(statusCode, responseBody, deserializedBody.Message);
            }
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
		
		async Task<IEnumerable<NodeListResponse>> ISwarmOperations.ListNodesAsync(CancellationToken cancellationToken)
        {
            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Get, $"nodes", cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<NodeListResponse[]>(response.Body);
        }

		
		async Task<NodeListResponse> ISwarmOperations.InspectNodeAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var response = await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Get, $"nodes/{id}", cancellationToken).ConfigureAwait(false);

            return this._client.JsonSerializer.DeserializeObject<NodeListResponse>(response.Body);
        }

		
		async Task<DockerApiResponse> ISwarmOperations.RemoveNodeAsync(string id, bool force, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            return await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Delete, $"nodes/{id}?force={force}", cancellationToken).ConfigureAwait(false);
        }
				
		 async Task<DockerApiResponse> ISwarmOperations.UpdateNodeAsync(string id, ulong version, NodeUpdateParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var query = new EnumerableQueryString(nameof(version), new[] { version.ToString() });
            var body = new JsonRequestContent<NodeUpdateParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)), this._client.JsonSerializer);
            return await this._client.MakeRequestAsync(new[] { SwarmResponseHandler }, HttpMethod.Post, $"nodes/{id}/update", query, body, cancellationToken);
        }
    }
}
