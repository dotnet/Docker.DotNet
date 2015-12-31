using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System.Linq;
using System.Text;

namespace Docker.DotNet
{
    internal class ContainerOperations : IContainerOperations
    {
        internal static readonly ApiResponseErrorHandlingDelegate NoSuchContainerHandler = (statusCode, responseBody) =>
        {
            if (statusCode == HttpStatusCode.NotFound)
            {
                throw new DockerContainerNotFoundException(statusCode, responseBody);
            }
        };

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
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerListResponse[]>(response.Body);
        }

        public async Task<ContainerResponse> InspectContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/json", id);
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerResponse>(response.Body);
        }

        public async Task<CreateContainerResponse> CreateContainerAsync(CreateContainerParameters parameters)
        {
            IQueryString qs = null;

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (!string.IsNullOrEmpty(parameters.ContainerName))
            {
                qs = new QueryString<CreateContainerParameters>(parameters);
            }

            string path = "containers/create";
            JsonRequestContent<Config> data = null;
            if (parameters.Config != null)
            {
                data = new JsonRequestContent<Config>(parameters.Config, this.Client.JsonSerializer);
            }
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, qs, data).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<CreateContainerResponse>(response.Body);
        }

        public Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/export", id);
            return this.Client.MakeRequestForStreamAsync(new[] {NoSuchContainerHandler}, HttpMethod.Get, path, null, null, null, cancellationToken);
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
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Get, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerProcessesResponse>(response.Body);
        }

        public async Task<IList<FilesystemChange>> InspectChangesAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/changes", id);
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<FilesystemChange[]>(response.Body);
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
                data = new JsonRequestContent<HostConfig>(hostConfig, this.Client.JsonSerializer);
            }
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, null, data).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotModified;
        }

        public async Task<ExecCreateContainerResponse> ExecCreateContainerAsync(string id, ExecCreateContainerParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/exec", id);
            JsonRequestContent<ExecCreateContainerConfig> data = null;
            if (parameters.Config != null)
            {
                data = new JsonRequestContent<ExecCreateContainerConfig>(parameters.Config, this.Client.JsonSerializer);
            }
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null, data).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ExecCreateContainerResponse>(response.Body);
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
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, queryParameters, null, new TimeSpan(Timeout.Infinite), cancellationToken).ConfigureAwait(false);
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
            return this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, queryParameters, null, new TimeSpan?(new TimeSpan(Timeout.Infinite)), cancellationToken);
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
            return this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, queryParameters);
        }

        public Task PauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/pause", id);
            return this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, null);
        }

        public Task UnpauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/unpause", id);
            return this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, null);
        }

        public async Task<WaitContainerResponse> WaitContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/wait", id);
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, null, null, new TimeSpan(Timeout.Infinite), cancellationToken).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<WaitContainerResponse>(response.Body);
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
            return this.Client.MakeRequestAsync(new[] {NoSuchContainerHandler}, HttpMethod.Delete, path, queryParameters);
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
            return this.Client.MakeRequestForStreamAsync(new[] {NoSuchContainerHandler}, HttpMethod.Get, path, queryParameters, null, cancellationToken);
        }

        [Obsolete("Deprecated since api version 1.20 in favor of the archive methods")]
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

            var data = new JsonRequestContent<CopyFromContainerParameters>(parameters, this.Client.JsonSerializer);

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/copy", id);
            return this.Client.MakeRequestForStreamAsync(new[] {NoSuchContainerHandler}, HttpMethod.Post, path, null, data, cancellationToken);
        }

        public async Task<GetArchiveFromContainerResponse> GetArchiveFromContainerAsync(string id, GetArchiveFromContainerParameters parameters, bool statOnly, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            IQueryString queryParameters = new QueryString<GetArchiveFromContainerParameters>(parameters);

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/archive", id);

            DockerApiStreamedResponse response = await this.Client.MakeRequestForStreamedResponseAsync(new[] { NoSuchContainerHandler }, statOnly ? HttpMethod.Head : HttpMethod.Get, path, queryParameters, null, cancellationToken);

            string statHeader = response.Headers.GetValues("X-Docker-Container-Path-Stat").First();

            byte[] bytes = Convert.FromBase64String(statHeader);

            string stat = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            ContainerPathStat pathStat = this.Client.JsonSerializer.DeserializeObject<ContainerPathStat>(stat);

            return new GetArchiveFromContainerResponse
            {
                Stat = pathStat,
                Stream = statOnly ? null : response.Body
            };
        }

        public Task ExtractArchiveToContainerAsync(string id, ExtractArchiveToContainerParameters parameters, Stream stream, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            IQueryString queryParameters = new QueryString<ExtractArchiveToContainerParameters>(parameters);

            BinaryRequestContent data = new BinaryRequestContent(stream, "application/x-tar");

            string path = string.Format(CultureInfo.InvariantCulture, "containers/{0}/archive", id);

            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Put, path, queryParameters, data, null, cancellationToken);
        }
    }
}