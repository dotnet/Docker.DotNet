using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

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

        private readonly DockerClient _client;

        internal ImageOperations(DockerClient client)
        {
            this._client = client;
        }

        public async Task<IList<ImagesListResponse>> ListImagesAsync(ImagesListParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ImagesListParameters>(parameters);
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "images/json", queryParameters).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ImagesListResponse[]>(response.Body);
        }

        public async Task<ImageInspectResponse> InspectImageAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var response = await this._client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Get, $"images/{name}/json", null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ImageInspectResponse>(response.Body);
        }

        public async Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var response = await this._client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Get, $"images/{name}/history", null).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ImageHistoryResponse[]>(response.Body);
        }


        public Task TagImageAsync(string name, ImageTagParameters parameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ImageTagParameters>(parameters);
            return this._client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Post, $"images/{name}/tag", queryParameters);
        }

        public async Task<IList<IDictionary<string, string>>> DeleteImageAsync(string name, ImageDeleteParameters parameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ImageDeleteParameters>(parameters);
            var response = await this._client.MakeRequestAsync(new[] { NoSuchImageHandler }, HttpMethod.Delete, $"images/{name}", queryParameters).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<Dictionary<string, string>[]>(response.Body);
        }

        public async Task<IList<ImageSearchResponse>> SearchImagesAsync(ImagesSearchParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ImagesSearchParameters>(parameters);
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "images/search", queryParameters).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<ImageSearchResponse[]>(response.Body);
        }

        public Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return PullImageAsync(new ImagesPullParameters() { All = false, Parent = parameters.Parent, RegistryAuth = parameters.RegistryAuth }, authConfig);
        }

        public Task<Stream> PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ImagesPullParameters>(parameters);
            return this._client.MakeRequestForStreamAsync(this._client.NoErrorHandlers, HttpMethod.Post, "images/create", queryParameters, RegistryAuthHeaders(authConfig), null, CancellationToken.None);
        }

        public Task<Stream> PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            IQueryString queryParameters = new QueryString<ImagePushParameters>(parameters);
            return this._client.MakeRequestForStreamAsync(this._client.NoErrorHandlers, HttpMethod.Post, $"images/{name}/push", queryParameters, RegistryAuthHeaders(authConfig), null, CancellationToken.None);
        }

        private Dictionary<string, string> RegistryAuthHeaders(AuthConfig authConfig)
        {
            return new Dictionary<string, string>
            {
                {
                    RegistryAuthHeaderKey,
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(this._client.JsonSerializer.SerializeObject(authConfig ?? new AuthConfig())))
                }
            };
        }
    }
}