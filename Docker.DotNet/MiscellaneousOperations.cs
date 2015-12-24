using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class MiscellaneousOperations : IMiscellaneousOperations
    {
        private DockerClient Client { get; set; }

        internal MiscellaneousOperations(DockerClient client)
        {
            this.Client = client;
        }

        public Task AuthenticateAsync(AuthConfig authConfig)
        {
            if (authConfig == null)
            {
                throw new ArgumentNullException("authConfig");
            }
            var data = new JsonRequestContent<AuthConfig>(authConfig, this.Client.JsonSerializer);

            const string path = "auth";
            return this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Post, path, null, data);
        }

        public async Task<VersionResponse> GetVersionAsync()
        {
            const string path = "version";
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<VersionResponse>(response.Body);
        }

        public Task PingAsync()
        {
            return this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, "_ping", null);
        }

        public async Task<SystemInfoResponse> GetSystemInfoAsync()
        {
            const string path = "info";
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, null).ConfigureAwait(false); ;
            return this.Client.JsonSerializer.DeserializeObject<SystemInfoResponse>(response.Body);
        }

        public Task<Stream> MonitorEventsAsync(MonitorDockerEventsParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            const string path = "events";
            IQueryString queryParameters = new QueryString<MonitorDockerEventsParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(this.Client.NoErrorHandlers, HttpMethod.Get, path, queryParameters, null, cancellationToken);
        }

        public async Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            JsonRequestContent<Config> data = parameters.Config == null ? null : new JsonRequestContent<Config>(parameters.Config, this.Client.JsonSerializer);
            const string path = "commit";
            IQueryString queryParameters = new QueryString<CommitContainerChangesParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Post, path, queryParameters, data).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<CommitContainerChangesResponse>(response.Body);
        }

        public Task<Stream> GetImageAsTarballAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}/get", name);
            return this.Client.MakeRequestForStreamAsync(new[] {ImageOperations.NoSuchImageHandler}, HttpMethod.Get, path, null, null, cancellationToken);
        }

        public Task LoadImageFromTarballAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            BinaryRequestContent data = new BinaryRequestContent(stream, "application/x-tar");
            const string path = "images/load";
            return this.Client.MakeRequestAsync(new[] {ImageOperations.NoSuchImageHandler}, HttpMethod.Post, path, null, data, null, cancellationToken);
        }

        public Task<Stream> BuildImageFromDockerfileAsync(Stream contents, BuildImageFromDockerfileParameters parameters, CancellationToken cancellationToken)
        {
            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            BinaryRequestContent data = new BinaryRequestContent(contents, "application/tar");
            const string path = "build";
            IQueryString queryParameters = new QueryString<BuildImageFromDockerfileParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(this.Client.NoErrorHandlers, HttpMethod.Post, path, queryParameters, data, cancellationToken);
        }
    }
}