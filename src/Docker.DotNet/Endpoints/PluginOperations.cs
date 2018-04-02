using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class PluginOperations : IPluginOperations
    {
        internal static readonly ApiResponseErrorHandlingDelegate NoSuchPluginHandler = (statusCode, responseBody) =>
        {
            if (statusCode == HttpStatusCode.NotFound)
            {
                throw new DockerPluginNotFoundException(statusCode, responseBody);
            }
        };

        private readonly DockerClient _client;
        private const string TarContentType = "application/x-tar";

        internal PluginOperations(DockerClient client)
        {
            this._client = client;
        }

        public Task InstallPluginAsync(PluginInstallParameters parameters, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.RequestBody == null)
            {
                throw new ArgumentNullException(nameof(parameters.RequestBody));
            }

            var data = new JsonRequestContent<IList<PluginPrivilege>>(parameters.RequestBody, this._client.JsonSerializer);
            
            IQueryString queryParameters = new QueryString<PluginInstallParameters>(parameters);
            return StreamUtil.MonitorStreamForMessagesAsync(
                this._client.MakeRequestForStreamAsync(this._client.NoErrorHandlers, HttpMethod.Post, $"plugins/pull", queryParameters, data, null, CancellationToken.None),
                this._client,
                cancellationToken,
                progress);
        }

        public async Task<IList<Plugin>> ListPluginsAsync(PluginListParameters parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryString queryParameters = parameters == null ? null : new QueryString<PluginListParameters>(parameters);
            var response = await this._client.MakeRequestAsync(this._client.NoErrorHandlers, HttpMethod.Get, "plugins", queryParameters, cancellationToken).ConfigureAwait(false);
            return this._client.JsonSerializer.DeserializeObject<Plugin[]>(response.Body);
        }

        public Task RemovePluginAsync(string name, PluginRemoveParameters parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = parameters == null ? null : new QueryString<PluginRemoveParameters>(parameters);
            return this._client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Delete, $"plugins/{name}", queryParameters, cancellationToken);
        }

        public Task EnablePluginAsync(string name, PluginEnableParameters parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = parameters == null ? null : new QueryString<PluginEnableParameters>(parameters);
            return this._client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Post, $"plugins/{name}/enable", queryParameters, cancellationToken);
        }

        public Task DisablePluginAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return this._client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Post, $"plugins/{name}/disable", cancellationToken);
        }

        public Task<Plugin> InspectPluginAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<PluginPrivilege>> GetPluginPrivilegesAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
