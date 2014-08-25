using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Docker.DotNet.Models;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Xml;
using System.Globalization;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Threading;
using System.Text;

namespace Docker.DotNet
{
	internal class ImageOperations : IImageOperations
	{
		private const string RegistryAuthHeaderKey = "X-Registry-Auth";

		private DockerClient Client { get; set; }

		internal ImageOperations (DockerClient client)
		{
			this.Client = client;
		}

		public async Task<IList<ImageListResponse>> ListImagesAsync (ListImagesParameters parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			string path = "images/json";
			IQueryString queryParameters = new QueryString<ListImagesParameters> (parameters);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, queryParameters);
			return this.Client.JsonConverter.DeserializeObject<ImageListResponse[]> (response.Body);
		}

		public async Task<ImageResponse> InspectImageAsync (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}/json", name);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, null);
			return this.Client.JsonConverter.DeserializeObject<ImageResponse> (response.Body);
		}

		public async Task<IList<ImageHistoryResponse>> GetImageHistoryAsync (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}/history", name);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, null);
			return this.Client.JsonConverter.DeserializeObject<ImageHistoryResponse[]> (response.Body);
		}


		public Task TagImageAsync (string name, TagImageParameters parameters)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}/tag", name);
			IQueryString queryParameters = new QueryString<TagImageParameters> (parameters);
			return this.Client.MakeRequestAsync (HttpMethod.Post, path, queryParameters);
		}

		public async Task<IList<IDictionary<string,string>>> DeleteImageAsync (string name, DeleteImageParameters parameters)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}", name);
			IQueryString queryParameters = new QueryString<DeleteImageParameters> (parameters);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Delete, path, queryParameters);
			return this.Client.JsonConverter.DeserializeObject<Dictionary<string,string>[]> (response.Body);
		}

		public async Task<IList<ImageSearchResponse>> SearchImagesAsync (SearchImagesParameters parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			// extra check since we know this parameter is required
			if (string.IsNullOrEmpty (parameters.Term)) {
				throw new ArgumentNullException ("parameters.Term");
			}

			string path = "images/search";
			IQueryString queryParameters = new QueryString<SearchImagesParameters> (parameters);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, queryParameters);
			return this.Client.JsonConverter.DeserializeObject<ImageSearchResponse[]> (response.Body);
		}

		public Task<Stream> CreateImageAsync (CreateImageParameters parameters, AuthConfig authConfig)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			Dictionary<string, string> headers = authConfig == null ? null : RegistryAuthHeaders (authConfig);

			string path = "images/create";
			IQueryString queryParameters = new QueryString<CreateImageParameters> (parameters);
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Post, path, queryParameters, headers, null, CancellationToken.None);
		}

		public Task<Stream> PushImageAsync (string name, PushImageParameters parameters, AuthConfig authConfig)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			if (authConfig == null) {
				throw new ArgumentNullException ("authConfig");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}/push", name);
			IQueryString queryParameters = new QueryString<PushImageParameters> (parameters);
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Post, path, queryParameters, RegistryAuthHeaders (authConfig), null, CancellationToken.None);
		}

		private Dictionary<string, string> RegistryAuthHeaders (AuthConfig authConfig)
		{
			if (authConfig == null) {
				throw new ArgumentNullException ("authConfig");
			}

			return new Dictionary<string, string> () { {RegistryAuthHeaderKey,
					Convert.ToBase64String (Encoding.UTF8.GetBytes (this.Client.JsonConverter.SerializeObject<AuthConfig> (authConfig)))
				}
			};
		}
	}
}