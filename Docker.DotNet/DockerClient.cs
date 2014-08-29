using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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

		private readonly ApiResponseErrorHandlingDelegate _defaultErrorHandlingDelegate = (statusCode, body) => {
			if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest) {
				throw new DockerApiException (statusCode, body);
			}
		};

		internal readonly IEnumerable<ApiResponseErrorHandlingDelegate> NoErrorHandlers = Enumerable.Empty<ApiResponseErrorHandlingDelegate> ();

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
			return this.Configuration.Credentials.BuildHttpClient ();
		}

		#region Convenience methods

		internal Task<DockerApiResponse> MakeRequestAsync (IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString)
		{
			return MakeRequestAsync (errorHandlers, method, path, queryString, null, null, CancellationToken.None);
		}

		internal Task<DockerApiResponse> MakeRequestAsync (IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IRequestContent data)
		{
			return MakeRequestAsync (errorHandlers, method, path, queryString, data, null, CancellationToken.None);
		}

		internal Task<Stream> MakeRequestForStreamAsync (IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IRequestContent data, CancellationToken cancellationToken)
		{
			return MakeRequestForStreamAsync (errorHandlers, method, path, queryString, null, data, cancellationToken);
		}

		#endregion


		#region HTTP Calls

		internal async Task<DockerApiResponse> MakeRequestAsync (IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IRequestContent data, TimeSpan? timeout, CancellationToken cancellationToken)
		{
			HttpResponseMessage response = await this.MakeRequestInnerAsync (null, HttpCompletionOption.ResponseContentRead, method, path, queryString, null, data, cancellationToken);
			string body = await response.Content.ReadAsStringAsync ();
			HandleIfErrorResponse (response.StatusCode, body, errorHandlers);
			return new DockerApiResponse (response.StatusCode, body);
		}

		internal async Task<Stream> MakeRequestForStreamAsync (IEnumerable<ApiResponseErrorHandlingDelegate> errorHandlers, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
		{
			HttpResponseMessage response = await MakeRequestInnerAsync (Timeout.InfiniteTimeSpan, HttpCompletionOption.ResponseHeadersRead, method, path, queryString, headers, data, cancellationToken);
			HandleIfErrorResponse (response.StatusCode, null, errorHandlers);
			return await response.Content.ReadAsStreamAsync ();
		}

		private async Task<HttpResponseMessage> MakeRequestInnerAsync (TimeSpan? requestTimeout, HttpCompletionOption completionOption, HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data, CancellationToken cancellationToken)
		{
			HttpClient client = this.GetHttpClient ();
			if (requestTimeout.HasValue) {
				client.Timeout = requestTimeout.Value;
			}

			HttpRequestMessage request = PrepareRequest (method, path, queryString, headers, data);
			return await client.SendAsync (request, completionOption, cancellationToken);
		}

		#endregion

		#region Error handling chain

		private void HandleIfErrorResponse (HttpStatusCode statusCode, string responseBody, IEnumerable<ApiResponseErrorHandlingDelegate> handlers)
		{
			if (handlers == null) {
				throw new ArgumentNullException ("handlers");
			}

			foreach (var handler in handlers) {
				handler (statusCode, responseBody);
			}
			_defaultErrorHandlingDelegate (statusCode, responseBody);
		}

		#endregion

		internal HttpRequestMessage PrepareRequest (HttpMethod method, string path, IQueryString queryString, IDictionary<string, string> headers, IRequestContent data)
		{
			if (string.IsNullOrEmpty ("path")) {
				throw new ArgumentNullException ("path");
			}

			HttpRequestMessage request = new HttpRequestMessage (method, HttpUtility.BuildUri (this.Configuration.EndpointBaseUri, path, queryString));
			request.Headers.Add ("User-Agent", UserAgent);
			if (headers != null) {
				foreach (var header in headers) {
					request.Headers.Add (header.Key, header.Value);
				}
			}

			if (data != null) {
				HttpContent requestContent = data.GetContent (); // make the call only once.
				request.Content = requestContent;
			}

			return request;
		}
	}

	internal delegate void ApiResponseErrorHandlingDelegate (HttpStatusCode statusCode, string responseBody);
}