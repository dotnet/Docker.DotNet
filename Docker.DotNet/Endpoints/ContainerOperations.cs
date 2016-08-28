using System;
using System.Collections.Generic;
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

        private readonly DockerClient _client;

        internal ContainerOperations(DockerClient client)
        {
            this._client = client;
        }

        public async Task<IList<ContainerListResponse>> ListContainersAsync(ContainersListParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainersListParameters>(parameters);
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "containers/json", queryParameters).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ContainerListResponse[]>(response.Body);
        }

        public async Task<ContainerInspectResponse> InspectContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, $"containers/{id}/json", null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ContainerInspectResponse>(response.Body);
        }

        public async Task<CreateContainerResponse> CreateContainerAsync(CreateContainerParameters parameters)
        {
            IQueryString qs = null;

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!string.IsNullOrEmpty(parameters.Name))
            {
                qs = new QueryString<CreateContainerParameters>(parameters);
            }

            var data = new JsonRequestContent<CreateContainerParameters>(parameters, this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, "containers/create", qs, data).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<CreateContainerResponse>(response.Body);
        }

        public Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this._client.MakeRequestForStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, $"containers/{id}/export", null, null, null, cancellationToken);
        }

        public async Task<ContainerProcessesResponse> ListProcessesAsync(string id, ContainerListProcessesParameters parameters)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainerListProcessesParameters>(parameters);
            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, $"containers/{id}/top", queryParameters).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ContainerProcessesResponse>(response.Body);
        }

        public async Task<IList<ContainerFileSystemChangeResponse>> InspectChangesAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, $"containers/{id}/changes", null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ContainerFileSystemChangeResponse[]>(response.Body);
        }

        public async Task<bool> StartContainerAsync(string id, HostConfig hostConfig)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            JsonRequestContent<HostConfig> data = null;
            if (hostConfig != null)
            {
                data = new JsonRequestContent<HostConfig>(hostConfig, this._client.JsonSerializer);
            }
            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/start", null, data).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotModified;
        }

        public async Task<ContainerExecCreateResponse> ExecCreateContainerAsync(string id, ContainerExecCreateParameters parameters)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = new JsonRequestContent<ContainerExecCreateParameters>(parameters, this._client.JsonSerializer);
            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/exec", null, data).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ContainerExecCreateResponse>(response.Body);
        }

        public async Task<bool> StopContainerAsync(string id, ContainerStopParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainerStopParameters>(parameters);
            // since specified wait timespan can be greater than HttpClient's default, we set the
            // client timeout to infinite and provide a cancellation token.
            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/stop", queryParameters, null, new TimeSpan(Timeout.Infinite), cancellationToken).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotModified;
        }

        public Task RestartContainerAsync(string id, ConatinerRestartParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }


            IQueryString queryParameters = new QueryString<ConatinerRestartParameters>(parameters);
            // since specified wait timespan can be greater than HttpClient's default, we set the
            // client timeout to infinite and provide a cancellation token.
            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/restart", queryParameters, null, new TimeSpan(Timeout.Infinite), cancellationToken);
        }

        public Task KillContainerAsync(string id, ContainerKillParameters parameters)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainerKillParameters>(parameters);
            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/kill", queryParameters);
        }

        public Task PauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/pause", null);
        }

        public Task UnpauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/unpause", null);
        }

        public async Task<ContainerWaitResponse> WaitContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/wait", null, null, new TimeSpan(Timeout.Infinite), cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ContainerWaitResponse>(response.Body);
        }

        public Task RemoveContainerAsync(string id, ContainerRemoveParameters parameters)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainerRemoveParameters>(parameters);
            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Delete, $"containers/{id}", queryParameters);
        }

        public Task<Stream> GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainerLogsParameters>(parameters);
            return this._client.MakeRequestForStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, $"containers/{id}/logs", queryParameters, null, cancellationToken);
        }

        public async Task<GetArchiveFromContainerResponse> GetArchiveFromContainerAsync(string id, GetArchiveFromContainerParameters parameters, bool statOnly, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<GetArchiveFromContainerParameters>(parameters);


            var response = await this._client.MakeRequestForStreamedResponseAsync(new[] { NoSuchContainerHandler }, statOnly ? HttpMethod.Head : HttpMethod.Get, $"containers/{id}/archive", queryParameters, null, cancellationToken);

            var statHeader = response.Headers.GetValues("X-Docker-Container-Path-Stat").First();

            var bytes = Convert.FromBase64String(statHeader);

            var stat = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            var pathStat = this._client.JsonSerializer.DeserializeObject<ContainerPathStatResponse>(stat);

            return new GetArchiveFromContainerResponse
            {
                Stat = pathStat,
                Stream = statOnly ? null : response.Body
            };
        }

        public Task ExtractArchiveToContainerAsync(string id, ContainerPathStatParameters parameters, Stream stream, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainerPathStatParameters>(parameters);

            var data = new BinaryRequestContent(stream, "application/x-tar");
            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Put, $"containers/{id}/archive", queryParameters, data, null, cancellationToken);
        }

        public async Task<MultiplexedStream> AttachContainerAsync(string id, bool tty, ContainerAttachParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryParameters = new QueryString<ContainerAttachParameters>(parameters);
            var stream = await this._client.MakeRequestForHijackedStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/attach", queryParameters, null, null, cancellationToken).ConfigureAwait(false);
            if (!stream.CanCloseWrite)
            {
                stream.Dispose();
                throw new NotSupportedException("Cannot shutdown write on this transport");
            }

            return new MultiplexedStream(stream, !tty);
        }

        public Task ResizeContainerTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryParameters = new QueryString<ContainerResizeParameters>(parameters);
            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/resize", queryParameters, null, null, cancellationToken);
        }

        // StartContainerExecAsync will start the process specified by id in detach mode with no connected
        // stdin, stdout, or stderr pipes.
        public Task StartContainerExecAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var parameters = new ContainerExecStartParameters
            {
                Detach = true,
            };
            var data = new JsonRequestContent<ContainerExecStartParameters>(parameters, this._client.JsonSerializer);
            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"exec/{id}/start", null, data, null, cancellationToken);
        }

        // StartAndAttachContainerExecAsync will start the process specified by id with stdin, stdout, stderr
        // connected, and optionally using terminal emulation if tty is true.
        public async Task<MultiplexedStream> StartAndAttachContainerExecAsync(string id, bool tty, CancellationToken cancellationToken)
        {
            return await StartWithConfigContainerExecAsync(id, new ExecConfig() { AttachStdin = true, AttachStderr = true, AttachStdout = true, Tty = tty }, cancellationToken);
        }

        public async Task<MultiplexedStream> StartWithConfigContainerExecAsync(string id, ExecConfig eConfig, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var data = new JsonRequestContent<ContainerExecStartParameters>(new ContainerExecStartParameters(eConfig), this._client.JsonSerializer);
            var stream = await this._client.MakeRequestForHijackedStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"exec/{id}/start", null, null, data, cancellationToken).ConfigureAwait(false);
            if (!stream.CanCloseWrite)
            {
                stream.Dispose();
                throw new NotSupportedException("Cannot shutdown write on this transport");
            }

            return new MultiplexedStream(stream, !eConfig.Tty);
        }

        public async Task<ContainerExecInspectResponse> InspectContainerExecAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, $"exec/{id}/json", null, null, null, cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ContainerExecInspectResponse>(response.Body);
        }

        public Task ResizeContainerExecTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryParameters = new QueryString<ContainerResizeParameters>(parameters);
            return this._client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"exec/{id}/resize", queryParameters, null, null, cancellationToken);
        }

        public Task<Stream> GetContainerStatsAsync(string id, ContainerStatsParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ContainerStatsParameters>(parameters);
            return this._client.MakeRequestForStreamAsync(this._client.NoErrorHandlers, HttpMethod.Get, $"containers/{id}/stats", queryParameters, null, cancellationToken);
        }
    }
}