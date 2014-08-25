using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Docker.DotNet.Models;
using System.Net.Http;
using System.Globalization;
using System.Net;
using System.Threading;
using System.IO;

namespace Docker.DotNet
{
	internal class ContainerOperations : IContainerOperations
	{
		private DockerClient Client { get; set; }

		internal ContainerOperations (DockerClient client)
		{
			this.Client = client;
		}

		public async Task<IList<ContainerListResponse>> ListContainersAsync (ListContainersParameters parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (parameters.All.HasValue) {
				queryParameters ["all"] = parameters.All;
			}

			if (parameters.Limit.HasValue) {
				queryParameters ["limit"] = parameters.Limit;
			}

			if (parameters.Sizes.HasValue) {
				queryParameters ["size"] = parameters.Sizes;
			}

			if (parameters.TimeFilter != null) {
				var constraint = parameters.TimeFilter;
				string key;
				switch (constraint.Mode) {
				case ListContainersParameters.TimeConstraintMode.Before:
					key = "before";
					break;
				case ListContainersParameters.TimeConstraintMode.Since:
					key = "since";
					break;
				default:
					throw new InvalidOperationException ("Unhandled time constraint mode");
				}
				queryParameters [key] = constraint.ContainerId;
			}

			string path = "containers/json";
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, queryParameters);
			return this.Client.JsonConverter.DeserializeObject<ContainerListResponse[]> (response.Body);
		}

		public async Task<ContainerResponse> InspectContainerAsync (string id)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/json", id);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, null);
			return this.Client.JsonConverter.DeserializeObject<ContainerResponse> (response.Body);
		}

		public Task<Stream> ExportContainerAsync (string id, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/export", id);
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Get, path, null, null, cancellationToken);
		}

		public async Task<ContainerProcessesResponse> ListProcessesAsync (string id, ListProcessesParameters parameters)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			if (parameters == null) { 
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (!string.IsNullOrEmpty (parameters.PsArgs)) {
				queryParameters ["ps_args"] = parameters.PsArgs;
			}
			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/top", id);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, queryParameters);
			return this.Client.JsonConverter.DeserializeObject<ContainerProcessesResponse> (response.Body);
		}

		public async Task<IList<FilesystemChange>> InspectChangesAsync (string id)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/changes", id);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, null);
			return this.Client.JsonConverter.DeserializeObject<FilesystemChange[]> (response.Body);
		}

		public async Task<bool> StartContainerAsync (string id, HostConfig hostConfig)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/start", id);
			JsonRequestContent<HostConfig> data = null;
			if (hostConfig != null) {
				data = new JsonRequestContent<HostConfig> (hostConfig, this.Client.JsonConverter);
			}
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Post, path, null, data);
			return response.StatusCode != HttpStatusCode.NotModified;
		}

		public async Task<bool> StopContainerAsync (string id, StopContainerParameters parameters, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (parameters.Wait.HasValue) {
				queryParameters ["t"] = parameters.Wait.Value.TotalSeconds;
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/stop", id);
			// since specified wait timespan can be greater than HttpClient's default, we set the
			// client timeout to infinite and provide a cancellation token.
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Post, path, queryParameters, null, Timeout.InfiniteTimeSpan, cancellationToken);
			return response.StatusCode != HttpStatusCode.NotModified;
		}

		public Task RestartContainerAsync (string id, RestartContainerParameters parameters, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (parameters.Wait.HasValue) {
				queryParameters ["t"] = parameters.Wait.Value.TotalSeconds;
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/restart", id);
			// since specified wait timespan can be greater than HttpClient's default, we set the
			// client timeout to infinite and provide a cancellation token.
			return this.Client.MakeRequestAsync (HttpMethod.Post, path, queryParameters, null, Timeout.InfiniteTimeSpan, cancellationToken);
		}

		public Task KillContainerAsync (string id, KillContainerParameters parameters)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (!string.IsNullOrEmpty (parameters.Signal)) {
				queryParameters ["signal"] = parameters.Signal; 
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/kill", id);
			return this.Client.MakeRequestAsync (HttpMethod.Post, path, queryParameters);
		}

		public Task PauseContainerAsync (string id)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/pause", id);
			return this.Client.MakeRequestAsync (HttpMethod.Post, path, null);
		}

		public Task UnpauseContainerAsync (string id)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/unpause", id);
			return this.Client.MakeRequestAsync (HttpMethod.Post, path, null);
		}

		public async Task<WaitContainerResponse> WaitContainerAsync (string id, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/wait", id);
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Post, path, null, null, Timeout.InfiniteTimeSpan, cancellationToken);
			return this.Client.JsonConverter.DeserializeObject<WaitContainerResponse> (response.Body);
		}

		public Task RemoveContainerAsync (string id, RemoveContainerParameters parameters)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (parameters.RemoveVolumes.HasValue) {
				queryParameters ["v"] = parameters.RemoveVolumes.Value;
			} 

			if (parameters.Force.HasValue) {
				queryParameters ["force"] = parameters.Force.Value;
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}", id);
			return this.Client.MakeRequestAsync (HttpMethod.Delete, path, queryParameters);
		}

		public Task<Stream> GetContainerLogsAsync (string id, GetContainerLogsParameters parameters, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}
			
			var queryParameters = new Dictionary<string, object> ();
			if (parameters.Stdout.HasValue) {
				queryParameters ["stdout"] = parameters.Stdout.Value;
			}

			if (parameters.Stderr.HasValue) {
				queryParameters ["stderr"] = parameters.Stderr.Value;
			}

			if (parameters.Timestamps.HasValue) {
				queryParameters ["timestamps"] = parameters.Timestamps.Value;
			}

			if (parameters.Tail != null) {
				queryParameters ["tail"] = parameters.Tail.Value;
			}

			if (parameters.Follow.HasValue) {
				queryParameters ["follow"] = parameters.Follow.Value;
			}

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/logs", id);
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Get, path, queryParameters, null, cancellationToken);
		}

		public Task<Stream> CopyFromContainerAsync (string id, CopyFromContainerParameters parameters, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty (id)) {
				throw new ArgumentNullException ("id");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			// Extra check for this field since it's required and API behaves and just returns
			// HTTP 500 Internal Server Error with response body as "EOF" which makes hard to debug.
			if (string.IsNullOrEmpty (parameters.Resource)) {
				throw new ArgumentNullException ("parameters.ResourcePath");
			}

			var data = new JsonRequestContent<CopyFromContainerParameters> (parameters, this.Client.JsonConverter);

			string path = string.Format (CultureInfo.InvariantCulture, "containers/{0}/copy", id);
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Post, path, null, data, cancellationToken);
		}
	}
}
