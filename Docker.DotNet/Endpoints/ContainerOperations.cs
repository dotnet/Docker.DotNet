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

        private DockerClient Client { get; set; }

        internal ContainerOperations(DockerClient client)
        {
            this.Client = client;
        }

        public async Task<IList<ContainerListResponse>> ListContainersAsync(ContainersListParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var path = "containers/json";
            IQueryString queryParameters = new QueryString<ContainersListParameters>(parameters);
            var response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerListResponse[]>(response.Body);
        }

        public async Task<ContainerInspectResponse> InspectContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"containers/{id}/json";
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerInspectResponse>(response.Body);
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

            var path = "containers/create";
            var data = new JsonRequestContent<CreateContainerParameters>(parameters, this.Client.JsonSerializer);
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, qs, data).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<CreateContainerResponse>(response.Body);
        }

        public Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"containers/{id}/export";
            return this.Client.MakeRequestForStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, path, null, null, null, cancellationToken);
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

            var path = $"containers/{id}/top";
            IQueryString queryParameters = new QueryString<ContainerListProcessesParameters>(parameters);
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerProcessesResponse>(response.Body);
        }

        public async Task<IList<ContainerFileSystemChangeResponse>> InspectChangesAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"containers/{id}/changes";
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerFileSystemChangeResponse[]>(response.Body);
        }

        public async Task<bool> StartContainerAsync(string id, HostConfig hostConfig)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"containers/{id}/start";
            JsonRequestContent<HostConfig> data = null;
            if (hostConfig != null)
            {
                data = new JsonRequestContent<HostConfig>(hostConfig, this.Client.JsonSerializer);
            }
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null, data).ConfigureAwait(false);
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

            var path = $"containers/{id}/exec";
            var data = new JsonRequestContent<ContainerExecCreateParameters>(parameters, this.Client.JsonSerializer);
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null, data).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerExecCreateResponse>(response.Body);
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

            var path = $"containers/{id}/stop";
            IQueryString queryParameters = new QueryString<ContainerStopParameters>(parameters);
            // since specified wait timespan can be greater than HttpClient's default, we set the
            // client timeout to infinite and provide a cancellation token.
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, queryParameters, null, new TimeSpan(Timeout.Infinite), cancellationToken).ConfigureAwait(false);
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


            var path = $"containers/{id}/restart";
            IQueryString queryParameters = new QueryString<ConatinerRestartParameters>(parameters);
            // since specified wait timespan can be greater than HttpClient's default, we set the
            // client timeout to infinite and provide a cancellation token.
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, queryParameters, null, new TimeSpan?(new TimeSpan(Timeout.Infinite)), cancellationToken);
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

            var path = $"containers/{id}/kill";
            IQueryString queryParameters = new QueryString<ContainerKillParameters>(parameters);
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, queryParameters);
        }

        public Task PauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"containers/{id}/pause";
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null);
        }

        public Task UnpauseContainerAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"containers/{id}/unpause";
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null);
        }

        public async Task<ContainerWaitResponse> WaitContainerAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"containers/{id}/wait";
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null, null, new TimeSpan(Timeout.Infinite), cancellationToken).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerWaitResponse>(response.Body);
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

            var path = $"containers/{id}";
            IQueryString queryParameters = new QueryString<ContainerRemoveParameters>(parameters);
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Delete, path, queryParameters);
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

            var path = $"containers/{id}/logs";
            IQueryString queryParameters = new QueryString<ContainerLogsParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, path, queryParameters, null, cancellationToken);
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

            var path = $"containers/{id}/archive";

            var response = await this.Client.MakeRequestForStreamedResponseAsync(new[] { NoSuchContainerHandler }, statOnly ? HttpMethod.Head : HttpMethod.Get, path, queryParameters, null, cancellationToken);

            var statHeader = response.Headers.GetValues("X-Docker-Container-Path-Stat").First();

            var bytes = Convert.FromBase64String(statHeader);

            var stat = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            var pathStat = this.Client.JsonSerializer.DeserializeObject<ContainerPathStatResponse>(stat);

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

            var path = $"containers/{id}/archive";

            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Put, path, queryParameters, data, null, cancellationToken);
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

            var path = $"containers/{id}/attach";
            var queryParameters = new QueryString<ContainerAttachParameters>(parameters);
            var stream = await this.Client.MakeRequestForHijackedStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, queryParameters, null, null, cancellationToken).ConfigureAwait(false);
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

            var path = $"containers/{id}/resize";
            var queryParameters = new QueryString<ContainerResizeParameters>(parameters);
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, queryParameters, null, null, cancellationToken);
        }

        // StartContainerExecAsync will start the process specified by id in detach mode with no connected
        // stdin, stdout, or stderr pipes.
        public Task StartContainerExecAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var path = $"exec/{id}/start";
            var parameters = new ContainerExecStartParameters
            {
                Detach = true,
            };
            var data = new JsonRequestContent<ContainerExecStartParameters>(parameters, this.Client.JsonSerializer);
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null, data, null, cancellationToken);
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

            var path = $"exec/{id}/start";
            var data = new JsonRequestContent<ContainerExecStartParameters>(new ContainerExecStartParameters(eConfig), this.Client.JsonSerializer);
            var stream = await this.Client.MakeRequestForHijackedStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, null, null, data, cancellationToken).ConfigureAwait(false);
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

            var path = $"exec/{id}/json";
            var response = await this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, path, null, null, null, cancellationToken).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ContainerExecInspectResponse>(response.Body);
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

            var path = $"exec/{id}/resize";
            var queryParameters = new QueryString<ContainerResizeParameters>(parameters);
            return this.Client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, path, queryParameters, null, null, cancellationToken);
        }
    }
}