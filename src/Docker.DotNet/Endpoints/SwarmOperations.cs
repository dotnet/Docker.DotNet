
namespace Docker.DotNet
{
    using System;
    using System.Net;
    using System.Net.Http;
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

        async Task<SwarmUnlockResponse> ISwarmOperations.GetSwarmUnlockKeyAsync()
        {
            var response = await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Get, "swarm/unlockkey", null, null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<SwarmUnlockResponse>(response.Body);
        }

        async Task<string> ISwarmOperations.InitSwarmAsync(SwarmInitParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var data = new JsonRequestContent<SwarmInitParameters>(parameters, this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/init", null, data).ConfigureAwait(false);
            return response.Body;
        }

        async Task<ClusterInfo> ISwarmOperations.InspectSwarmAsync()
        {
            var response = await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Get, "swarm", null, null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ClusterInfo>(response.Body);
        }

        async Task ISwarmOperations.JoinSwarmAsync(SwarmJoinParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var data = new JsonRequestContent<SwarmJoinParameters>(parameters, this._client.JsonSerializer);
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
            IQueryString query = null;
            if (parameters != null)
            {
                query = new QueryString<SwarmLeaveParameters>(parameters);
            }

            await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/leave", query, null).ConfigureAwait(false);
        }

        async Task ISwarmOperations.UnlockSwarmAsync(SwarmUnlockParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var body = new JsonRequestContent<SwarmUnlockParameters>(parameters, this._client.JsonSerializer);
            await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/unlock", null, body).ConfigureAwait(false);
        }

        async Task ISwarmOperations.UpdateSwarmAsync(SwarmUpdateParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (parameters.Spec == null) throw new ArgumentNullException(nameof(parameters.Spec));
            if (parameters.Version == 0) throw new ArgumentOutOfRangeException(nameof(parameters.Version));

            var query = new QueryString<SwarmUpdateParameters>(parameters);
            var body = new JsonRequestContent<Spec>(parameters.Spec, this._client.JsonSerializer);
            await this._client.MakeRequestAsync(new []{ SwarmResponseHandler }, HttpMethod.Post, "swarm/update", query, body).ConfigureAwait(false);
        }
    }
}