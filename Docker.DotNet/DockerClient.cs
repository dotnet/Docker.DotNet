using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet
{
    public sealed class DockerClient : IDockerClient
    {
        private const string UserAgent = "Docker.DotNet";

        private static TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

        private Version RequestedApiVersion { get; set; }

        public DockerClientConfiguration Configuration { get; private set; }

        internal JsonSerializer JsonSerializer { get; private set; }

        public IImageOperations Images { get; private set; }

        public IContainerOperations Containers { get; private set; }

        public IMiscellaneousOperations Miscellaneous { get; private set; }

        private readonly ApiResponseErrorHandlingDelegate _defaultErrorHandlingDelegate = (statusCode, body) =>
        {
            if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest)
            {
                throw new DockerApiException(statusCode, body);
            }
        };

        private readonly HttpClient _client;
        private readonly TimeSpan _defaultTimeout;

        internal readonly IEnumerable<ApiResponseErrorHandlingDelegate> NoErrorHandlers = Enumerable.Empty<ApiResponseErrorHandlingDelegate>();

        internal DockerClient(DockerClientConfiguration configuration, Version requestedApiVersion)
        {
            Configuration = configuration;
            RequestedApiVersion = requestedApiVersion;
            JsonSerializer = new JsonSerializer();

            Images = new ImageOperations(this);
            Containers = new ContainerOperations(this);
            Miscellaneous = new MiscellaneousOperations(this);

            _client = new HttpClient(Configuration.Credentials.Handler, false);

            _defaultTimeout = _client.Timeout;

            _client.Timeout = InfiniteTimeout;
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
            HttpResponseMessage response = await MakeRequestInnerAsync(null, HttpCompletionOption.ResponseContentRead, method, path, queryString, null, data, cancellationToken).ConfigureAwait(false);

            string body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            HandleIfErrorResponse(response.StatusCode, body, errorHandlers);

            return new DockerApiResponse(response.StatusCode, body);
        }

        internal async Task<Stream> MakeRequestForStreamAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await MakeRequestInnerAsync(InfiniteTimeout, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, data, cancellationToken).ConfigureAwait(false);

            HandleIfErrorResponse(response.StatusCode, null, errorHandlers);

            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        internal async Task<DockerApiStreamedResponse> MakeRequestForStreamedResponseAsync(IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await MakeRequestInnerAsync(TimeSpan.FromMilliseconds(Timeout.Infinite), HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, data, cancellationToken);
            HandleIfErrorResponse(response.StatusCode, null, errorHandlers);

            Stream body = await response.Content.ReadAsStreamAsync();

            return new DockerApiStreamedResponse(response.StatusCode, body, response.Headers);
        }

        private Task<HttpResponseMessage> MakeRequestInnerAsync(TimeSpan? requestTimeout, HttpCompletionOption completionOption, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = PrepareRequest(method, path, queryString, headers, data);

            if (requestTimeout.HasValue)
            {
                if (requestTimeout.Value != InfiniteTimeout)
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
            CancellationTokenSource timeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);

            timeoutTokenSource.CancelAfter(timeout);

            return timeoutTokenSource.Token;
        }

        #endregion

        #region Error handling chain

        private void HandleIfErrorResponse(HttpStatusCode statusCode, string responseBody, IEnumerable<ApiResponseErrorHandlingDelegate> handlers)
        {
            if (handlers == null)
            {
                throw new ArgumentNullException("handlers");
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
                throw new ArgumentNullException("path");
            }

            HttpRequestMessage request = new HttpRequestMessage(method, HttpUtility.BuildUri(Configuration.EndpointBaseUri, RequestedApiVersion, path, queryString));

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
                HttpContent requestContent = data.GetContent(); // make the call only once.
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