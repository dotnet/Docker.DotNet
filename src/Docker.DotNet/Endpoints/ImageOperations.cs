using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet
{
    internal class ImageOperations : IImageOperations
    {
        internal static readonly ApiResponseErrorHandlingDelegate NoSuchImageHandler = (statusCode, responseBody) =>
        {
            if (statusCode == HttpStatusCode.NotFound)
            {
                throw new DockerImageNotFoundException(statusCode, responseBody);
            }
        };

        private const string RegistryAuthHeaderKey = "X-Registry-Auth";
        private const string TarContentType = "application/x-tar";
        private const string ImportFromBodySource = "-";

        private readonly DockerClient _client;

        internal ImageOperations(DockerClient client)
        {
            _client = client;
        }

        public async Task<IList<ImagesListResponse>> ListImagesAsync(ImagesListParameters parameters, CancellationToken cancellationToken = default)
        {
            IQueryString queryParameters = new QueryString<ImagesListParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Get, "images/json", queryParameters, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<ImagesListResponse[]>(response.Body);
        }

        public Task<Stream> BuildImageFromDockerfileAsync(Stream contents, ImageBuildParameters parameters, CancellationToken cancellationToken = default)
        {
            if (contents == null)
            {
                throw new ArgumentNullException(nameof(contents));
            }

            var data = new BinaryRequestContent(contents, TarContentType);
            IQueryString queryParameters = new QueryString<ImageBuildParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            return _client.MakeRequestForStreamAsync(_client.NoErrorHandlers, HttpMethod.Post, "build", queryParameters, data, cancellationToken);
        }

        public Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default)
        {
            return CreateImageAsync(parameters, null, authConfig, progress, cancellationToken);
        }

        public Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IDictionary<string, string> headers, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default)
        {
            return CreateImageAsync(parameters, null, authConfig, headers, progress, cancellationToken);
        }

        public Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, Stream imageStream, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default)
        {
            return CreateImageAsync(parameters, imageStream, authConfig, null, progress, cancellationToken);
        }

        public Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, Stream imageStream, AuthConfig authConfig, IDictionary<string, string> headers, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var httpMethod = HttpMethod.Post;
            BinaryRequestContent content = null;
            if (imageStream != null)
            {
                content = new BinaryRequestContent(imageStream, TarContentType);
                parameters.FromSrc = ImportFromBodySource;
            }

            IQueryString queryParameters = new QueryString<ImagesCreateParameters>(parameters);

            var customHeaders = RegistryAuthHeaders(authConfig);

            if (headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    customHeaders[key] = headers[key];
                }
            }

            return StreamUtil.MonitorResponseForMessagesAsync(
                _client.MakeRequestForRawResponseAsync(httpMethod, "images/create", queryParameters, content, customHeaders, cancellationToken),
                _client,
                cancellationToken,
                progress);
        }

        public async Task<ImageInspectResponse> InspectImageAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var response = await _client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Get, $"images/{name}/json", cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<ImageInspectResponse>(response.Body);
        }

        public async Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var response = await _client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Get, $"images/{name}/history", cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<ImageHistoryResponse[]>(response.Body);
        }

        public Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = new QueryString<ImagePushParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            return StreamUtil.MonitorStreamForMessagesAsync(
                _client.MakeRequestForStreamAsync(_client.NoErrorHandlers, HttpMethod.Post, $"images/{name}/push", queryParameters, null, RegistryAuthHeaders(authConfig), CancellationToken.None),
                _client,
                cancellationToken,
                progress);
        }

        public Task<DockerApiResponse> TagImageAsync(string name, ImageTagParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = new QueryString<ImageTagParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            return _client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Post, $"images/{name}/tag", queryParameters, cancellationToken);
        }

        public async Task<IList<IDictionary<string, string>>> DeleteImageAsync(string name, ImageDeleteParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = new QueryString<ImageDeleteParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            var response = await _client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Delete, $"images/{name}", queryParameters, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<Dictionary<string, string>[]>(response.Body);
        }

        public async Task<IList<ImageSearchResponse>> SearchImagesAsync(ImagesSearchParameters parameters, CancellationToken cancellationToken = default)
        {
            IQueryString queryParameters = new QueryString<ImagesSearchParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Get, "images/search", queryParameters, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<ImageSearchResponse[]>(response.Body);
        }

        public async Task<ImagesPruneResponse> PruneImagesAsync(ImagesPruneParameters parameters, CancellationToken cancellationToken)
        {
            var queryParameters = parameters == null ? null : new QueryString<ImagesPruneParameters>(parameters);
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Post, "images/prune", queryParameters, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<ImagesPruneResponse>(response.Body);
        }

        public async Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var data = parameters.Config == null
                ? null
                : new JsonRequestContent<Config>(parameters.Config, _client.JsonSerializer);

            IQueryString queryParameters = new QueryString<CommitContainerChangesParameters>(parameters);
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Post, "commit", queryParameters, data, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<CommitContainerChangesResponse>(response.Body);
        }

        public Task<Stream> SaveImageAsync(string name, CancellationToken cancellationToken = default)
        {
            return SaveImagesAsync(new[] { name }, cancellationToken);
        }

        public Task<Stream> SaveImagesAsync(string[] names, CancellationToken cancellationToken = default)
        {
            EnumerableQueryString queryString = null;

            if (names?.Length > 0)
            {
                queryString = new EnumerableQueryString(nameof(names), names);
            }

            return _client.MakeRequestForStreamAsync(new[] { ImageOperations.NoSuchImageHandler }, HttpMethod.Get, "images/get", queryString, cancellationToken);
        }

        public Task LoadImageAsync(ImageLoadParameters parameters, Stream imageStream, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default)
        {
            if (imageStream == null)
            {
                throw new ArgumentNullException(nameof(imageStream));
            }

            var content = new BinaryRequestContent(imageStream, TarContentType);

            IQueryString queryParameters = new QueryString<ImageLoadParameters>(parameters ?? throw new ArgumentNullException(nameof(parameters)));

            return StreamUtil.MonitorStreamForMessagesAsync(
                _client.MakeRequestForStreamAsync(_client.NoErrorHandlers, HttpMethod.Post, "images/load", queryParameters, content, cancellationToken),
                _client,
                cancellationToken,
                progress);
        }

        private Dictionary<string, string> RegistryAuthHeaders(AuthConfig authConfig)
        {
            return new Dictionary<string, string>
            {
                {
                    RegistryAuthHeaderKey,
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(_client.JsonSerializer.SerializeObject(authConfig ?? new AuthConfig())))
                }
            };
        }
    }
}
