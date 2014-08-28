using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class ContainerOperations : IContainerOperations
    {
        private DockerClient Client { get; set; }

        internal ContainerOperations(DockerClient client)
        {
            this.Client = client;
        }

        public async Task<IList<ContainerListResponse>> ListContainersAsync(ListContainersParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = "containers/json";
            IQueryString queryParameters = new QueryString<ListContainersParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Get, path, queryParameters);
            return this.Client.JsonConverter.DeserializeObject<ContainerListResponse[]>(response.Body);
        }

        public async Task<ContainerResponse> InspectContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/json", id);
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Get, path, null);
            return this.Client.JsonConverter.DeserializeObject<ContainerResponse>(response.Body);
        }

        public async Task<CreateContainerResponse> CreateContainerAsync(CreateContainerParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = "containers/create";
            JsonRequestContent<Config> data = parameters.Config == null ? null : new JsonRequestContent<Config>(parameters.Config, this.Client.JsonConverter);
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Post, path, null, data);
            return this.Client.JsonConverter.DeserializeObject<CreateContainerResponse>(response.Body);
        }

        public Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/export", id);
            return this.Client.MakeRequestForStreamAsync(HttpMethod.Get, path, null, null, cancellationToken);
        }

        public async Task<ContainerProcessesResponse> ListProcessesAsync(string id, ListProcessesParameters parameters)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/top", id);
            IQueryString queryParameters = new QueryString<ListProcessesParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Get, path, queryParameters);
            return this.Client.JsonConverter.DeserializeObject<ContainerProcessesResponse>(response.Body);
        }

        public async Task<IList<FilesystemChange>> InspectChangesAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/changes", id);
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Get, path, null);
            return this.Client.JsonConverter.DeserializeObject<FilesystemChange[]>(response.Body);
        }

        public async Task<bool> StartContainerAsync(string id, HostConfig hostConfig)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/start", id);
            JsonRequestContent<HostConfig> data = null;
            if (hostConfig != null)
            {
                data = new JsonRequestContent<HostConfig>(hostConfig, this.Client.JsonConverter);
            }
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Post, path, null, data);
            return response.StatusCode != HttpStatusCode.NotModified;
        }

        public async Task<bool> StopContainerAsync(string id, StopContainerParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/stop", id);
            IQueryString queryParameters = new QueryString<StopContainerParameters>(parameters);
            // since specified wait timespan can be greater than HttpClient's default, we set the
            // client timeout to infinite and provide a cancellation token.
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Post, path, queryParameters, null, Timeout.InfiniteTimeSpan, cancellationToken);
            return response.StatusCode != HttpStatusCode.NotModified;
        }

        public Task RestartContainerAsync(string id, RestartContainerParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }


            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/restart", id);
            IQueryString queryParameters = new QueryString<RestartContainerParameters>(parameters);
            // since specified wait timespan can be greater than HttpClient's default, we set the
            // client timeout to infinite and provide a cancellation token.
            return this.Client.MakeRequestAsync(HttpMethod.Post, path, queryParameters, null, Timeout.InfiniteTimeSpan, cancellationToken);
        }

        public Task KillContainerAsync(string id, KillContainerParameters parameters)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/kill", id);
            IQueryString queryParameters = new QueryString<KillContainerParameters>(parameters);
            return this.Client.MakeRequestAsync(HttpMethod.Post, path, queryParameters);
        }

        public Task PauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/pause", id);
            return this.Client.MakeRequestAsync(HttpMethod.Post, path, null);
        }

        public Task UnpauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/unpause", id);
            return this.Client.MakeRequestAsync(HttpMethod.Post, path, null);
        }

        public async Task<WaitContainerResponse> WaitContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/wait", id);
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Post, path, null, null, Timeout.InfiniteTimeSpan, cancellationToken);
            return this.Client.JsonConverter.DeserializeObject<WaitContainerResponse>(response.Body);
        }

        public Task RemoveContainerAsync(string id, RemoveContainerParameters parameters)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}", id);
            IQueryString queryParameters = new QueryString<RemoveContainerParameters>(parameters);
            return this.Client.MakeRequestAsync(HttpMethod.Delete, path, queryParameters);
        }

        public Task<Stream> GetContainerLogsAsync(string id, GetContainerLogsParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/logs", id);
            IQueryString queryParameters = new QueryString<GetContainerLogsParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(HttpMethod.Get, path, queryParameters, null, cancellationToken);
        }

        public Task<Stream> CopyFromContainerAsync(string id, CopyFromContainerParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            // Extra check for this field since it's required and API behaves and just returns
            // HTTP 500 Internal Server Error with response body as "EOF" which makes hard to debug.
            if (string.IsNullOrEmpty(parameters.Resource))
            {
                throw new ArgumentException("ResourcePath is empty", "parameters");
            }

            var data = new JsonRequestContent<CopyFromContainerParameters>(parameters, this.Client.JsonConverter);

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/copy", id);
            return this.Client.MakeRequestForStreamAsync(HttpMethod.Post, path, null, data, cancellationToken);
        }
    }
}