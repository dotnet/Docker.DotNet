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

#if NETSTANDARD1_6
using System.Net.Sockets;
#endif

namespace Docker.DotNet
{
    public sealed class DockerClient : IDockerClient
    {
        private const string UserAgent = "Docker.DotNet";

        private static readonly TimeSpan s_InfiniteTimeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

        public DockerClientConfiguration Configuration { get; }

        internal JsonSerializer JsonSerializer { get; private set; }

        public IImageOperations Images { get; }

        public IContainerOperations Containers { get; }

        public IMiscellaneousOperations Miscellaneous { get; }

        public INetworkOperations Networks { get; private set; }

        private readonly ApiResponseErrorHandlingDelegate _defaultErrorHandlingDelegate = (statusCode, body) =>
        {
            if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest)
            {
                throw new DockerApiException(statusCode, body);
            }
        };

        private readonly HttpClient _client;
        private readonly TimeSpan _defaultTimeout;

        private readonly Uri _endpointBaseUri;

        internal readonly IEnumerable<ApiResponseErrorHandlingDelegate> NoErrorHandlers = Enumerable.Empty<ApiResponseErrorHandlingDelegate>();
        private readonly Version _requestedApiVersion;

        internal DockerClient(DockerClientConfiguration configuration, Version requestedApiVersion)
        {
            Configuration = configuration;
            _requestedApiVersion = requestedApiVersion;
            JsonSerializer = new JsonSerializer();

            Images = new ImageOperations(this);
            Containers = new ContainerOperations(this);
            Miscellaneous = new MiscellaneousOperations(this);
            Networks = new NetworkOperations(this);

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
                        throw new ArgumentException($"{Configuration.EndpointBaseUri} is not a valid npipe URI");
                    }

                    var serverName = uri.Host;
                    var pipeName = uri.Segments[2];

                    uri = new UriBuilder("http", pipeName).Uri;
                    handler = new ManagedHandler(async (host, port, cancellationToken) =>
                    {
                        // NamedPipeClientStream handles file not found by polling until the server arrives. Use a short
                        // timeout so that the user doesn't get stuck waiting for a dockerd instance that is not running.
                        var timeout = 100; // 100ms
                        var stream = new NamedPipeClientStream(serverName, pipeName);
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

#if NETSTANDARD1_6
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

            _client = new HttpClient(Configuration.Credentials.GetHandler(handler), true);
            _defaultTimeout = _client.Timeout;
            _client.Timeout = s_InfiniteTimeout;
        }

        #region Convenience methods

        internal Task<DockerApiResponse> MakeRequestAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString)
        {
            return MakeRequestAsync(errorHandlers, method, path, queryString, null, null, CancellationToken.None);
        }

        internal Task<DockerApiResponse> MakeRequestAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IRequestContent data)
        {
            return MakeRequestAsync(errorHandlers, method, path, queryString, data, null, CancellationToken.None);
        }

        internal Task<Stream> MakeRequestForStreamAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IRequestContent data, CancellationToken cancellationToken)
        {
            return MakeRequestForStreamAsync(errorHandlers, method, path, queryString, null, data, cancellationToken);
        }

        internal Task<DockerApiStreamedResponse> MakeRequestForStreamedResponseAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IRequestContent data, CancellationToken cancellationToken)
        {
            return MakeRequestForStreamedResponseAsync(errorHandlers, method, path, queryString, null, data, cancellationToken);
        }

        #endregion

        #region HTTP Calls

        internal async Task<DockerApiResponse> MakeRequestAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IRequestContent data, TimeSpan? timeout, CancellationToken cancellationToken)
        {
            var response = await MakeRequestInnerAsync(null, HttpCompletionOption.ResponseContentRead, method, path, queryString, null, data, cancellationToken).ConfigureAwait(false);

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            HandleIfErrorResponse(response.StatusCode, body, errorHandlers);

            return new DockerApiResponse(response.StatusCode, body);
        }

        internal async Task<Stream> MakeRequestForStreamAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
        {
            var response = await MakeRequestInnerAsync(s_InfiniteTimeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, data, cancellationToken).ConfigureAwait(false);

            HandleIfErrorResponse(response.StatusCode, null, errorHandlers);

            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        internal async Task<DockerApiStreamedResponse> MakeRequestForStreamedResponseAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
        {
            var response = await MakeRequestInnerAsync(s_InfiniteTimeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, data, cancellationToken);
            HandleIfErrorResponse(response.StatusCode, null, errorHandlers);

            var body = await response.Content.ReadAsStreamAsync();

            return new DockerApiStreamedResponse(response.StatusCode, body, response.Headers);
        }

        internal async Task<WriteClosableStream> MakeRequestForHijackedStreamAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
        {
            var response = await MakeRequestInnerAsync(s_InfiniteTimeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, data, cancellationToken).ConfigureAwait(false);

            HandleIfErrorResponse(response.StatusCode, null, errorHandlers);

            var content = response.Content as HttpConnectionResponseContent;
            if (content == null)
            {
                throw new NotSupportedException("message handler does not support hijacked streams");
            }

            return content.HijackStream();
        }

        private Task<HttpResponseMessage> MakeRequestInnerAsync(TimeSpan? requestTimeout, HttpCompletionOption completionOption, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
        {
            var request = PrepareRequest(method, path, queryString, headers, data);

            if (requestTimeout.HasValue)
            {
                if (requestTimeout.Value != s_InfiniteTimeout)
                {
                    cancellationToken = CreateTimeoutToken(cancellationToken, requestTimeout.Value);
                }
            }
            else
            {
                cancellationToken = CreateTimeoutToken(cancellationToken, _defaultTimeout);
            }

            return _client.SendAsync(request, completionOption, cancellationToken);
        }

        private CancellationToken CreateTimeoutToken(CancellationToken token, TimeSpan timeout)
        {
            var timeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);

            timeoutTokenSource.CancelAfter(timeout);

            return timeoutTokenSource.Token;
        }

        #endregion

        #region Error handling chain

        private void HandleIfErrorResponse(HttpStatusCode statusCode, string responseBody, IEnumerable<ApiResponseErrorHandlingDelegate> handlers)
        {
            if (handlers == null)
            {
                throw new ArgumentNullException(nameof(handlers));
            }

            foreach (var handler in handlers)
            {
                handler(statusCode, responseBody);
            }

            _defaultErrorHandlingDelegate(statusCode, responseBody);
        }

        #endregion

        internal HttpRequestMessage PrepareRequest(HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data)
        {
            if (string.IsNullOrEmpty("path"))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var request = new HttpRequestMessage(method, HttpUtility.BuildUri(_endpointBaseUri, this._requestedApiVersion, path, queryString));

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

        public void Dispose()
        {
            Configuration.Dispose();
        }
    }

    internal delegate void ApiResponseErrorHandlingDelegate(HttpStatusCode statusCode, string responseBody);
}