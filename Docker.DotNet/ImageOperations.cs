using System;
using System.Collections.Generic;
using System.Globalization;
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

        private DockerClient Client { get; set; }

        internal ImageOperations(DockerClient client)
        {
            this.Client = client;
        }

        public async Task<IList<ImageListResponse>> ListImagesAsync(ListImagesParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = "images/json";
            IQueryString queryParameters = new QueryString<ListImagesParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ImageListResponse[]>(response.Body);
        }

        public async Task<ImageResponse> InspectImageAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}/json", name);
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchImageHandler}, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ImageResponse>(response.Body);
        }

        public async Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}/history", name);
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchImageHandler}, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ImageHistoryResponse[]>(response.Body);
        }


        public Task TagImageAsync(string name, TagImageParameters parameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}/tag", name);
            IQueryString queryParameters = new QueryString<TagImageParameters>(parameters);
            return this.Client.MakeRequestAsync(new[] {NoSuchImageHandler}, HttpMethod.Post, path, queryParameters);
        }

        public async Task<IList<IDictionary<string, string>>> DeleteImageAsync(string name, DeleteImageParameters parameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}", name);
            IQueryString queryParameters = new QueryString<DeleteImageParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(new[] {NoSuchImageHandler}, HttpMethod.Delete, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<Dictionary<string, string>[]>(response.Body);
        }

        public async Task<IList<ImageSearchResponse>> SearchImagesAsync(SearchImagesParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = "images/search";
            IQueryString queryParameters = new QueryString<SearchImagesParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, queryParameters).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<ImageSearchResponse[]>(response.Body);
        }

        public Task<Stream> CreateImageAsync(CreateImageParameters parameters, AuthConfig authConfig)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            string path = "images/create";
            Dictionary<string, string> headers = authConfig == null ? null : RegistryAuthHeaders(authConfig);
            IQueryString queryParameters = new QueryString<CreateImageParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(this.Client.NoErrorHandlers, HttpMethod.Post, path, queryParameters, headers, null, CancellationToken.None);
        }

        public Task<Stream> PushImageAsync(string name, PushImageParameters parameters, AuthConfig authConfig)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (authConfig == null)
            {
                throw new ArgumentNullException("authConfig");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}/push", name);
            IQueryString queryParameters = new QueryString<PushImageParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(this.Client.NoErrorHandlers, HttpMethod.Post, path, queryParameters, RegistryAuthHeaders(authConfig), null, CancellationToken.None);
        }

        private Dictionary<string, string> RegistryAuthHeaders(AuthConfig authConfig)
        {
            if (authConfig == null)
            {
                throw new ArgumentNullException("authConfig");
            }

            return new Dictionary<string, string>
            {
                {
                    RegistryAuthHeaderKey,
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Client.JsonSerializer.SerializeObject<AuthConfig>(authConfig)))
                }
            };
        }
    }
}