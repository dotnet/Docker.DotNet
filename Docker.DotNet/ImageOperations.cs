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

			var query = new Dictionary<string, object> ();
			if (parameters.All.HasValue) {
				query ["all"] = parameters.All.Value;
			}

			if (parameters.Filters != null) {
				string filtersJsonEncoded = this.Client.JsonConverter.SerializeObject (parameters.Filters);
				query ["filters"] = filtersJsonEncoded;
			}

			string path = "images/json";
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, query);
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

			// extra check since it's required
			if (string.IsNullOrEmpty (parameters.Repo)) {
				throw new ArgumentNullException ("parameters.Repo");
			}

			var queryParameters = new Dictionary<string,object> () {
				{ "repo", parameters.Repo }
			};
			if (parameters.Force.HasValue) {
				queryParameters ["force"] = parameters.Force.Value;
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}/tag", name);
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

			var queryParameters = new Dictionary<string,object> ();
			if (parameters.Force.HasValue) {
				queryParameters ["force"] = parameters.Force.Value;
			}

			if (parameters.NoPrune.HasValue) {
				queryParameters ["noprune"] = parameters.NoPrune.Value;
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}", name);
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

			var queryParameters = new Dictionary<string,object> () {
				{ "term", parameters.Term }
			};

			string path = "images/search";
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, queryParameters);
			return this.Client.JsonConverter.DeserializeObject<ImageSearchResponse[]> (response.Body);
		}

		public Task<Stream> CreateImageAsync (CreateImageParameters parameters, AuthConfig authConfig)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string,object> ();
			if (!string.IsNullOrEmpty (parameters.FromImage)) {
				queryParameters ["fromImage"] = parameters.FromImage;
			}

			if (!string.IsNullOrEmpty (parameters.Tag)) {
				queryParameters ["tag"] = parameters.Tag;
			}

			if (!string.IsNullOrEmpty (parameters.Repo)) {
				queryParameters ["repo"] = parameters.Repo;
			}

			if (!string.IsNullOrEmpty (parameters.Registry)) {
				queryParameters ["registry"] = parameters.Registry;
			}

			Dictionary<string, string> headers = null;
			if (authConfig != null) {
				headers = RegistryAuthHeaders (authConfig);
			}

			string path = "images/create";
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

			var queryParameters = new Dictionary<string,object> ();
			if (!string.IsNullOrEmpty (parameters.Tag)) {
				queryParameters ["tag"] = parameters.Tag;
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}/push", name);
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