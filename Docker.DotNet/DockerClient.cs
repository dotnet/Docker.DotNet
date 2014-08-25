using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Net.Http.Headers;
using System.IO;

namespace Docker.DotNet
{
	public sealed class DockerClient
	{
		private const string UserAgent = "Docker.DotNet";

		public DockerClientConfiguration Configuration { get; private set; }

		internal JsonConverter JsonConverter { get; private set; }

		public IImageOperations Images { get; private set; }

		public IContainerOperations Containers { get; private set; }

		public IMiscellaneousOperations Miscellaneous { get; private set; }

		internal DockerClient (DockerClientConfiguration configuration)
		{
			this.Configuration = configuration;
			this.JsonConverter = new JsonConverter ();

			this.Images = new ImageOperations (this);
			this.Containers = new ContainerOperations (this);
			this.Miscellaneous = new MiscellaneousOperations (this);
		}

		private HttpClient GetHttpClient ()
		{
			return new HttpClient (); //TODO extract into factory?
		}

		internal Task<DockerAPIResponse> MakeRequestAsync (HttpMethod method, string path, IDictionary<string, object> queryParameters)
		{
			return MakeRequestAsync (method, path, queryParameters, null, null, CancellationToken.None);
		}

		internal Task<DockerAPIResponse> MakeRequestAsync (HttpMethod method, string path, IDictionary<string, object> queryParameters, IRequestContent data)
		{
			return MakeRequestAsync (method, path, queryParameters, data, null, CancellationToken.None);
		}

		internal async Task<DockerAPIResponse> MakeRequestAsync (HttpMethod method, string path, IDictionary<string, object> queryParameters, IRequestContent data, TimeSpan? timeout, CancellationToken cancellationToken)
		{
			HttpClient client = this.GetHttpClient ();
			if (timeout.HasValue) {
				client.Timeout = timeout.Value;
			}

			HttpRequestMessage request = PrepareRequest (method, path, queryParameters, null, data);
			HttpResponseMessage response = await client.SendAsync (request, cancellationToken);

			string body = await response.Content.ReadAsStringAsync ();
			HttpStatusCode statusCode = response.StatusCode;

			if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest) {
				throw new DockerAPIException (statusCode, body);
			}

			return new DockerAPIResponse (statusCode, body);
		}

		internal Task<Stream> MakeRequestForStreamAsync (HttpMethod method, string path, IDictionary<string, object> queryParameters, IRequestContent data, CancellationToken cancellationToken)
		{
			return MakeRequestForStreamAsync (method, path, queryParameters, null, data, cancellationToken);
		}

		internal async Task<Stream> MakeRequestForStreamAsync (HttpMethod method, string path, IDictionary<string, object> queryParameters, IDictionary<string,string> headers, IRequestContent data, CancellationToken cancellationToken)
		{
			HttpClient client = this.GetHttpClient ();
			client.Timeout = Timeout.InfiniteTimeSpan; // stream indefinitely (termination via EOF or cancellation)

			HttpRequestMessage request = PrepareRequest (method, path, queryParameters, headers, data);
			HttpResponseMessage response = await client.SendAsync (request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

			HttpStatusCode statusCode = response.StatusCode;
			if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest) {
				throw new DockerAPIException (statusCode, null);
			}

			return await response.Content.ReadAsStreamAsync ();
		}

		internal HttpRequestMessage PrepareRequest (HttpMethod method, string path, IDictionary<string, object> queryParameters, IDictionary<string,string> headers, IRequestContent data)
		{
			if (string.IsNullOrEmpty ("path")) {
				throw new ArgumentNullException ("path");
			}

			HttpRequestMessage request = new HttpRequestMessage (method, HttpUtility.BuildUri (this.Configuration.EndpointBaseUri, path, queryParameters));
			request.Headers.Add ("User-Agent", UserAgent);
			if (headers != null) {
				foreach (var header in headers) {
					request.Headers.Add (header.Key, header.Value);
				}
			}

			if (data != null) {
				var requestContent = data.GetContent (); // get once.
				request.Content = requestContent;
			}

			return request;
		}
	}
}

