using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class ExecOperations : IExecOperations
    {
        internal static readonly ApiResponseErrorHandlingDelegate NoSuchContainerHandler = (statusCode, responseBody) =>
        {
            if (statusCode == HttpStatusCode.NotFound)
            {
                throw new DockerContainerNotFoundException(statusCode, responseBody);
            }
        };

        private readonly DockerClient _client;

        internal ExecOperations(DockerClient client)
        {
            _client = client;
        }

        public async Task<ContainerExecCreateResponse> ExecCreateContainerAsync(string id, ContainerExecCreateParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = new JsonRequestContent<ContainerExecCreateParameters>(parameters, _client.JsonSerializer);
            var response = await _client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"containers/{id}/exec", null, data, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<ContainerExecCreateResponse>(response.Body);
        }

        public async Task<ContainerExecInspectResponse> InspectContainerExecAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var response = await _client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Get, $"exec/{id}/json", null, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<ContainerExecInspectResponse>(response.Body);
        }

        public Task ResizeContainerExecTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken = default)
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
            return _client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"exec/{id}/resize", queryParameters, cancellationToken);
        }

        // StartAndAttachContainerExecAsync will start the process specified by id with stdin, stdout, stderr
        // connected, and optionally using terminal emulation if tty is true.
        public Task<MultiplexedStream> StartAndAttachContainerExecAsync(string id, bool tty, CancellationToken cancellationToken = default)
        {
            return StartWithConfigContainerExecAsync(id, new ContainerExecStartParameters() { AttachStdin = true, AttachStderr = true, AttachStdout = true, Tty = tty }, cancellationToken);
        }

        // StartContainerExecAsync will start the process specified by id in detach mode with no connected
        // stdin, stdout, or stderr pipes.
        public Task StartContainerExecAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var parameters = new ContainerExecStartParameters
            {
                Detach = true,
            };
            var data = new JsonRequestContent<ContainerExecStartParameters>(parameters, _client.JsonSerializer);
            return _client.MakeRequestAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"exec/{id}/start", null, data, cancellationToken);
        }

        public async Task<MultiplexedStream> StartWithConfigContainerExecAsync(string id, ContainerExecStartParameters eConfig, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var data = new JsonRequestContent<ContainerExecStartParameters>(eConfig, _client.JsonSerializer);
            var stream = await _client.MakeRequestForHijackedStreamAsync(new[] { NoSuchContainerHandler }, HttpMethod.Post, $"exec/{id}/start", null, data, null, cancellationToken).ConfigureAwait(false);
            if (!stream.CanCloseWrite)
            {
                stream.Dispose();
                throw new NotSupportedException("Cannot shutdown write on this transport");
            }

            return new MultiplexedStream(stream, !eConfig.Tty);
        }
    }
}
