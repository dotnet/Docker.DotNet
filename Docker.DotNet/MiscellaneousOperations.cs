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
            var data = new JsonRequestContent<AuthConfig>(authConfig, this.Client.JsonConverter);

            const string path = "auth";
            return this.Client.MakeRequestAsync(HttpMethod.Post, path, null, data);
        }

        public async Task<VersionResponse> GetVersionAsync()
        {
            const string path = "version";
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Get, path, null);
            return this.Client.JsonConverter.DeserializeObject<VersionResponse>(response.Body);
        }

        public Task PingAsync()
        {
            return this.Client.MakeRequestAsync(HttpMethod.Get, "_ping", null);
        }

        public async Task<SystemInfoResponse> GetSystemInfoAsync()
        {
            const string path = "info";
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Get, path, null);
            return this.Client.JsonConverter.DeserializeObject<SystemInfoResponse>(response.Body);
        }

        public Task<Stream> MonitorEventsAsync(MonitorDockerEventsParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            const string path = "events";
            IQueryString queryParameters = new QueryString<MonitorDockerEventsParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(HttpMethod.Get, path, queryParameters, null, cancellationToken);
        }

        public async Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            JsonRequestContent<Config> data = parameters.Config == null ? null : new JsonRequestContent<Config>(parameters.Config, this.Client.JsonConverter);
            const string path = "commit";
            IQueryString queryParameters = new QueryString<CommitContainerChangesParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(HttpMethod.Post, path, queryParameters, data);
            return this.Client.JsonConverter.DeserializeObject<CommitContainerChangesResponse>(response.Body);
        }

        public Task<Stream> GetImageAsTarballAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}/get", name);
            return this.Client.MakeRequestForStreamAsync(HttpMethod.Get, path, null, null, cancellationToken);
        }

        public Task LoadImageFromTarballAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            BinaryRequestContent data = new BinaryRequestContent(stream, "application/x-tar");
            const string path = "images/load";
            return this.Client.MakeRequestAsync(HttpMethod.Post, path, null, data, null, cancellationToken);
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
            return this.Client.MakeRequestForStreamAsync(HttpMethod.Post, path, queryParameters, data, cancellationToken);
        }
    }
}