using System;
using Docker.DotNet.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Globalization;

namespace Docker.DotNet
{
	internal class MiscellaneousOperations : IMiscellaneousOperations
	{
		private DockerClient Client { get; set; }

		internal MiscellaneousOperations (DockerClient client)
		{
			this.Client = client;
		}

		public Task AuthenticateAsync (AuthConfig authConfig)
		{
			if (authConfig == null) {
				throw new ArgumentNullException ("authConfig");
			}
			var data = new JsonRequestContent<AuthConfig> (authConfig, this.Client.JsonConverter);

			string path = "auth";
			return this.Client.MakeRequestAsync (HttpMethod.Post, path, null, data);
		}

		public async Task<VersionResponse> GetVersionAsync ()
		{
			string path = "version";
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, null);
			return this.Client.JsonConverter.DeserializeObject<VersionResponse> (response.Body);
		}

		public Task PingAsync ()
		{
			return this.Client.MakeRequestAsync (HttpMethod.Get, "_ping", null);
		}

		public async Task<SystemInfoResponse> GetSystemInfoAsync ()
		{
			string path = "info";
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Get, path, null);
			return this.Client.JsonConverter.DeserializeObject<SystemInfoResponse> (response.Body);
		}

		public Task<Stream> MonitorEventsAsync (MonitorDockerEventsParameters parameters, CancellationToken cancellationToken)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (parameters.Since.HasValue) {
				queryParameters ["since"] = parameters.Since.Value;
			}

			if (parameters.Until.HasValue) {
				queryParameters ["until"] = parameters.Until.Value;
			}

			string path = "events";
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Get, path, queryParameters, null, cancellationToken);
		}

		public async Task<CommitContainerChangesResponse> CommitContainerChangesAsync (CommitContainerChangesParameters parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			// extra check for container id since we know it's required (otherwise returns container not found error)
			if (string.IsNullOrEmpty (parameters.ContainerId)) {
				throw new ArgumentNullException ("parameters.ContainerId");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (!string.IsNullOrEmpty (parameters.ContainerId)) {
				queryParameters ["container"] = parameters.ContainerId;
			}

			if (!string.IsNullOrEmpty (parameters.Repo)) {
				queryParameters ["repo"] = parameters.Repo;
			}

			if (!string.IsNullOrEmpty (parameters.Message)) {
				queryParameters ["m"] = parameters.Message;
			}

			if (!string.IsNullOrEmpty (parameters.Tag)) {
				queryParameters ["tag"] = parameters.Tag;
			}

			if (!string.IsNullOrEmpty (parameters.Author)) {
				queryParameters ["author"] = parameters.Author;
			}

			JsonRequestContent<Config> data = null;
			if (parameters.Config != null) {
				data = new JsonRequestContent<Config> (parameters.Config, this.Client.JsonConverter);
			}

			string path = "commit";
			DockerAPIResponse response = await this.Client.MakeRequestAsync (HttpMethod.Post, path, queryParameters, data);
			return this.Client.JsonConverter.DeserializeObject<CommitContainerChangesResponse> (response.Body);
		}

		public Task<Stream> GetImageAsTarballAsync (string name, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty (name)) {
				throw new ArgumentNullException ("name");
			}

			string path = string.Format (CultureInfo.InvariantCulture, "images/{0}/get", name);
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Get, path, null, null, cancellationToken);
		}

		public Task LoadImageFromTarballAsync (Stream stream)
		{ //TODO cancellationtoken
			if (stream == null) {
				throw new ArgumentNullException ("stream");
			}

			BinaryRequestContent data = new BinaryRequestContent (stream, "application/x-tar");
			string path = "images/load";
			return this.Client.MakeRequestAsync (HttpMethod.Post, path, null, data);
		}

		public Task<Stream> BuildImageFromDockerfileAsync (Stream contents, BuildImageFromDockerfileParameters parameters, CancellationToken cancellationToken)
		{
			if (contents == null) {
				throw new ArgumentNullException ("contents");
			}

			if (parameters == null) {
				throw new ArgumentNullException ("parameters");
			}

			var queryParameters = new Dictionary<string, object> ();
			if (parameters.Quiet.HasValue) {
				queryParameters ["q"] = parameters.Quiet.Value;
			}

			if (parameters.NoCache.HasValue) {
				queryParameters ["nocache"] = parameters.NoCache.Value;
			}

			if (parameters.RemoveIntermediateContainers.HasValue) {
				queryParameters ["rm"] = parameters.RemoveIntermediateContainers.Value;
			}

			if (parameters.ForceRemoveIntermediateContainers.HasValue) {
				queryParameters ["forcerm"] = parameters.ForceRemoveIntermediateContainers.Value;
			}

			if (!string.IsNullOrEmpty(parameters.RepositoryTagName)) {
				queryParameters ["t"] = parameters.RepositoryTagName;
			}

			BinaryRequestContent data = new BinaryRequestContent (contents, "application/tar");
			string path = "build";
			return this.Client.MakeRequestForStreamAsync (HttpMethod.Post, path, queryParameters, data, cancellationToken);
		}
	}
}

