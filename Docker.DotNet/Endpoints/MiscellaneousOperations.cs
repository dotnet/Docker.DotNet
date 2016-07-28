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
                throw new ArgumentNullException(nameof(authConfig));
            }
            var data = new JsonRequestContent<AuthConfig>(authConfig, this.Client.JsonSerializer);
            
            return this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Post, "auth", null, data);
        }

        public async Task<VersionResponse> GetVersionAsync()
        {            
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, "version", null).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<VersionResponse>(response.Body);
        }

        public Task PingAsync()
        {
            return this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, "_ping", null);
        }

        public async Task<SystemInfoResponse> GetSystemInfoAsync()
        {            
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Get, "info", null).ConfigureAwait(false); ;
            return this.Client.JsonSerializer.DeserializeObject<SystemInfoResponse>(response.Body);
        }

        public Task<Stream> MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
           
            IQueryString queryParameters = new QueryString<ContainerEventsParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(this.Client.NoErrorHandlers, HttpMethod.Get, "events", queryParameters, null, cancellationToken);
        }

        public async Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            JsonRequestContent<Config> data = parameters.Config == null ? null : new JsonRequestContent<Config>(parameters.Config, this.Client.JsonSerializer);
                        
            IQueryString queryParameters = new QueryString<CommitContainerChangesParameters>(parameters);
            DockerApiResponse response = await this.Client.MakeRequestAsync(this.Client.NoErrorHandlers, HttpMethod.Post, "commit", queryParameters, data).ConfigureAwait(false);
            return this.Client.JsonSerializer.DeserializeObject<CommitContainerChangesResponse>(response.Body);
        }

        public Task<Stream> GetImageAsTarballAsync(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string path = string.Format(CultureInfo.InvariantCulture, "images/{0}/get", name);
            return this.Client.MakeRequestForStreamAsync(new[] {ImageOperations.NoSuchImageHandler}, HttpMethod.Get, path, null, null, cancellationToken);
        }

        public Task LoadImageFromTarballAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            BinaryRequestContent data = new BinaryRequestContent(stream, "application/x-tar");            
            return this.Client.MakeRequestAsync(new[] {ImageOperations.NoSuchImageHandler}, HttpMethod.Post, "images/load", null, data, null, cancellationToken);
        }

        public Task<Stream> BuildImageFromDockerfileAsync(Stream contents, ImageBuildParameters parameters, CancellationToken cancellationToken)
        {
            if (contents == null)
            {
                throw new ArgumentNullException(nameof(contents));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            BinaryRequestContent data = new BinaryRequestContent(contents, "application/tar");                        
            IQueryString queryParameters = new QueryString<ImageBuildParameters>(parameters);
            return this.Client.MakeRequestForStreamAsync(this.Client.NoErrorHandlers, HttpMethod.Post, "build", queryParameters, data, cancellationToken);
        }
    }
}