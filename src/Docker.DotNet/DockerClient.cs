using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Net.Http.Client;

#if (NETSTANDARD1_6 || NETSTANDARD2_0)

using System.Net.Sockets;

#endif

namespace Docker.DotNet
{
    internal delegate void ApiResponseErrorHandlingDelegate(HttpStatusCode statusCode, string responseBody);

    public sealed class DockerClient : IDockerClient
    {
        internal readonly IEnumerable<ApiResponseErrorHandlingDelegate> NoErrorHandlers = Enumerable.Empty<ApiResponseErrorHandlingDelegate>();

        private const string UserAgent = "Docker.DotNet";

        private static readonly TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

        private readonly Uri _endpointBaseUri;
        private readonly HttpClient _httpClient;
        private readonly Version _requestedApiVersion;

        internal DockerClient(DockerClientConfiguration configuration, Version requestedApiVersion)
        {
            Configuration = configuration;
            _requestedApiVersion = requestedApiVersion;
            JsonSerializer = new JsonSerializer();

            Images = new ImageOperations(this);
            Containers = new ContainerOperations(this);
            System = new SystemOperations(this);
            Networks = new NetworkOperations(this);
            Secrets = new SecretsOperations(this);
            Swarm = new SwarmOperations(this);
            Tasks = new TasksOperations(this);
            Volumes = new VolumeOperations(this);
            Plugin = new PluginOperations(this);
            Exec = new ExecOperations(this);

            ManagedHandler handler;
            var uri = Configuration.EndpointBaseUri;
            switch (uri.Scheme.ToLowerInvariant())
            {
                case "npipe":
                    if (Configuration.Credentials.IsTlsCredentials())
                    {
                        throw new Exception("TLS not supported over npipe");
                    }

                    var segments = uri.Segments;
                    if (segments.Length != 3 || !segments[1].Equals("pipe/", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new ArgumentException($"{nameof(Configuration)}.{nameof(Configuration.EndpointBaseUri)} is not a valid npipe URI");
                    }

                    var serverName = uri.Host;
                    if (string.Equals(serverName, "localhost", StringComparison.OrdinalIgnoreCase))
                    {
                        // npipe schemes dont work with npipe://localhost/... and need npipe://./... so fix that for a client here.
                        serverName = ".";
                    }

                    var pipeName = uri.Segments[2];

                    uri = new UriBuilder("http", pipeName).Uri;
                    handler = new ManagedHandler(async (host, port, cancellationToken) =>
                    {
                        int timeout = (int)Configuration.NamedPipeConnectTimeout.TotalMilliseconds;
                        var stream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
                        var dockerStream = new DockerPipeStream(stream);

#if NET45
                        await Task.Run(() => stream.Connect(timeout), cancellationToken);
#else
                        await stream.ConnectAsync(timeout, cancellationToken);
#endif
                        return dockerStream;
                    });

                    break;

                case "tcp":
                case "http":
                    var builder = new UriBuilder(uri)
                    {
                        Scheme = configuration.Credentials.IsTlsCredentials() ? "https" : "http"
                    };
                    uri = builder.Uri;
                    handler = new ManagedHandler();
                    break;

                case "https":
                    handler = new ManagedHandler();
                    break;

#if (NETSTANDARD1_6 || NETSTANDARD2_0)
                case "unix":
                    var pipeString = uri.LocalPath;
                    handler = new ManagedHandler(async (string host, int port, CancellationToken cancellationToken) =>
                    {
                        var sock = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
                        await sock.ConnectAsync(new UnixDomainSocketEndPoint(pipeString));
                        return sock;
                    });
                    uri = new UriBuilder("http", uri.Segments.Last()).Uri;
                    break;
#endif

                default:
                    throw new Exception($"Unknown URL scheme {configuration.EndpointBaseUri.Scheme}");
            }

            _endpointBaseUri = uri;

            _httpClient = new HttpClient(Configuration.Credentials.GetHandler(handler), true);
            DefaultTimeout = Configuration.DefaultTimeout;
            _httpClient.Timeout = InfiniteTimeout;
        }

        public DockerClientConfiguration Configuration { get; }

        public IContainerOperations Containers { get; }

        public TimeSpan DefaultTimeout { get; set; }

        public IExecOperations Exec { get; }

        public IImageOperations Images { get; }

        public INetworkOperations Networks { get; }

        public IPluginOperations Plugin { get; }

        public ISecretsOperations Secrets { get; }

        public ISwarmOperations Swarm { get; }

        public ISystemOperations System { get; }

        public ITasksOperations Tasks { get; }

        public IVolumeOperations Volumes { get; }

        internal JsonSerializer JsonSerializer { get; }

        public void Dispose()
        {
            Configuration.Dispose();
            _httpClient.Dispose();
        }

        public async Task HandleIfErrorResponseAsync(HttpStatusCode statusCode, HttpResponseMessage response)
        {
            bool isErrorResponse = statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest;

            string responseBody = null;

            if (isErrorResponse)
            {
                // If it is not an error response, we do not read the response body because the caller may wish to consume it.
                // If it is an error response, we do because there is nothing else going to be done with it anyway and
                // we want to report the response body in the error message as it contains potentially useful info.
                responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            // No custom handler was fired. Default the response for generic success/failures.
            if (isErrorResponse)
            {
                throw new DockerApiException(statusCode, responseBody);
            }
        }

        internal Task<DockerApiResponse> MakeRequestAsync(
                            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            CancellationToken token)
        {
            return MakeRequestAsync(errorHandlers, method, path, null, null, token);
        }

        internal Task<DockerApiResponse> MakeRequestAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            CancellationToken token)
        {
            return MakeRequestAsync(errorHandlers, method, path, queryString, null, token);
        }

        internal Task<DockerApiResponse> MakeRequestAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            CancellationToken token)
        {
            return MakeRequestAsync(errorHandlers, method, path, queryString, body, null, token);
        }

        internal Task<DockerApiResponse> MakeRequestAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            IDictionary<string, string> headers,
            CancellationToken token)
        {
            return MakeRequestAsync(errorHandlers, method, path, queryString, body, headers, DefaultTimeout, token);
        }

        internal async Task<DockerApiResponse> MakeRequestAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            IDictionary<string, string> headers,
            TimeSpan timeout,
            CancellationToken token)
        {
            var response = await PrivateMakeRequestAsync(timeout, HttpCompletionOption.ResponseContentRead, method, path, queryString, headers, body, token).ConfigureAwait(false);
            using (response)
            {
                await HandleIfErrorResponseAsync(response.StatusCode, response, errorHandlers);

                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return new DockerApiResponse(response.StatusCode, responseBody);
            }
        }

        internal Task<WriteClosableStream> MakeRequestForHijackedStreamAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            IDictionary<string, string> headers,
            CancellationToken cancellationToken)
        {
            return MakeRequestForHijackedStreamAsync(errorHandlers, method, path, queryString, body, headers, InfiniteTimeout, cancellationToken);
        }

        internal async Task<WriteClosableStream> MakeRequestForHijackedStreamAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            IDictionary<string, string> headers,
            TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            var response = await PrivateMakeRequestAsync(timeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, body, cancellationToken).ConfigureAwait(false);

            await HandleIfErrorResponseAsync(response.StatusCode, response, errorHandlers);

            if (!(response.Content is HttpConnectionResponseContent content))
            {
                throw new NotSupportedException("message handler does not support hijacked streams");
            }

            return content.HijackStream();
        }

        internal async Task<HttpResponseMessage> MakeRequestForRawResponseAsync(
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            IDictionary<string, string> headers,
            CancellationToken token)
        {
            var response = await PrivateMakeRequestAsync(InfiniteTimeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, body, token).ConfigureAwait(false);
            return response;
        }

        internal Task<Stream> MakeRequestForStreamAsync(
                                    IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            CancellationToken token)
        {
            return MakeRequestForStreamAsync(errorHandlers, method, path, null, token);
        }

        internal Task<Stream> MakeRequestForStreamAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            CancellationToken token)
        {
            return MakeRequestForStreamAsync(errorHandlers, method, path, queryString, null, token);
        }

        internal Task<Stream> MakeRequestForStreamAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            CancellationToken token)
        {
            return MakeRequestForStreamAsync(errorHandlers, method, path, queryString, body, null, token);
        }

        internal Task<Stream> MakeRequestForStreamAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            IDictionary<string, string> headers,
            CancellationToken token)
        {
            return MakeRequestForStreamAsync(errorHandlers, method, path, queryString, body, headers, InfiniteTimeout, token);
        }

        internal async Task<Stream> MakeRequestForStreamAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IRequestContent body,
            IDictionary<string, string> headers,
            TimeSpan timeout,
            CancellationToken token)
        {
            var response = await PrivateMakeRequestAsync(timeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, body, token).ConfigureAwait(false);

            await HandleIfErrorResponseAsync(response.StatusCode, response, errorHandlers);

            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        internal async Task<DockerApiStreamedResponse> MakeRequestForStreamedResponseAsync(
            IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers,
            HttpMethod method,
            string path,
            IQueryString queryString,
            CancellationToken cancellationToken)
        {
            var response = await PrivateMakeRequestAsync(InfiniteTimeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, null, null, cancellationToken);

            await HandleIfErrorResponseAsync(response.StatusCode, response, errorHandlers);

            var body = await response.Content.ReadAsStreamAsync();

            return new DockerApiStreamedResponse(response.StatusCode, body, response.Headers);
        }

        internal HttpRequestMessage PrepareRequest(HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var request = new HttpRequestMessage(method, HttpUtility.BuildUri(_endpointBaseUri, _requestedApiVersion, path, queryString))
            {
                Version = new Version(1, 1)
            };

            request.Headers.Add("User-Agent", UserAgent);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (data != null)
            {
                var requestContent = data.GetContent(); // make the call only once.
                request.Content = requestContent;
            }

            return request;
        }

        private async Task HandleIfErrorResponseAsync(HttpStatusCode statusCode, HttpResponseMessage response, IEnumerable<ApiResponseErrorHandlingDelegate> handlers)
        {
            bool isErrorResponse = statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest;

            string responseBody = null;

            if (isErrorResponse)
            {
                // If it is not an error response, we do not read the response body because the caller may wish to consume it.
                // If it is an error response, we do because there is nothing else going to be done with it anyway and
                // we want to report the response body in the error message as it contains potentially useful info.
                responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            // If no customer handlers just default the response.
            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    handler(statusCode, responseBody);
                }
            }

            // No custom handler was fired. Default the response for generic success/failures.
            if (isErrorResponse)
            {
                throw new DockerApiException(statusCode, responseBody);
            }
        }

        private async Task<HttpResponseMessage> PrivateMakeRequestAsync(
            TimeSpan timeout,
            HttpCompletionOption completionOption,
            HttpMethod method,
            string path,
            IQueryString queryString,
            IDictionary<string, string> headers,
            IRequestContent data,
            CancellationToken cancellationToken)
        {
            // If there is a timeout, we turn it into a cancellation token. At the same time, we need to link to the caller's
            // cancellation token. To avoid leaking objects, we must then also dispose of the CancellationTokenSource. To keep
            // code flow simple, we treat it as re-entering the same method with a different CancellationToken and no timeout.
            if (timeout != InfiniteTimeout)
            {
                using (var timeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
                {
                    timeoutTokenSource.CancelAfter(timeout);

                    // We must await here because we need to dispose of the CTS only after the work has been completed.
                    return await PrivateMakeRequestAsync(InfiniteTimeout, completionOption, method, path, queryString, headers, data, timeoutTokenSource.Token).ConfigureAwait(false);
                }
            }

            var request = PrepareRequest(method, path, queryString, headers, data);
            return await _httpClient.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false);
        }
    }
}
