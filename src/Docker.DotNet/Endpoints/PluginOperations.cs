using System;
using System.Collections.Generic;
using System.IO;
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

        private const string TarContentType = "application/x-tar";

        private readonly DockerClient _client;

        internal PluginOperations(DockerClient client)
        {
            _client = client;
        }

        public Task ConfigurePluginAsync(string name, PluginConfigureParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Args == null)
            {
                throw new ArgumentNullException($"{nameof(parameters)}.{nameof(parameters.Args)}");
            }

            var body = new JsonRequestContent<IList<string>>(parameters.Args, _client.JsonSerializer);
            return _client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Post, $"plugins/{name}/set", null, body, cancellationToken);
        }

        public Task CreatePluginAsync(PluginCreateParameters parameters, Stream plugin, CancellationToken cancellationToken = default)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (plugin == null)
            {
                throw new ArgumentNullException(nameof(plugin));
            }

            var query = new QueryString<PluginCreateParameters>(parameters);
            var data = new BinaryRequestContent(plugin, TarContentType);
            return _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Post, "plugins/create", query, data, cancellationToken);
        }

        public Task DisablePluginAsync(string name, PluginDisableParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = parameters == null ? null : new QueryString<PluginDisableParameters>(parameters);
            return _client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Post, $"plugins/{name}/disable", queryParameters, cancellationToken);
        }

        public Task EnablePluginAsync(string name, PluginEnableParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = parameters == null ? null : new QueryString<PluginEnableParameters>(parameters);
            return _client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Post, $"plugins/{name}/enable", queryParameters, cancellationToken);
        }

        public async Task<IList<PluginPrivilege>> GetPluginPrivilegesAsync(PluginGetPrivilegeParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var query = new QueryString<PluginGetPrivilegeParameters>(parameters);
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Get, "plugins/privileges", query, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<PluginPrivilege[]>(response.Body);
        }

        public async Task<Plugin> InspectPluginAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var response = await _client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Get, $"plugins/{name}/json", cancellationToken);
            return _client.JsonSerializer.DeserializeObject<Plugin>(response.Body);
        }

        public Task InstallPluginAsync(PluginInstallParameters parameters, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Privileges == null)
            {
                throw new ArgumentNullException($"{nameof(parameters)}.{nameof(parameters.Privileges)}");
            }

            var data = new JsonRequestContent<IList<PluginPrivilege>>(parameters.Privileges, _client.JsonSerializer);

            IQueryString queryParameters = new QueryString<PluginInstallParameters>(parameters);
            return StreamUtil.MonitorStreamForMessagesAsync(
                _client.MakeRequestForStreamAsync(_client.NoErrorHandlers, HttpMethod.Post, "plugins/pull", queryParameters, data, null, CancellationToken.None),
                cancellationToken,
                progress);
        }

        public async Task<IList<Plugin>> ListPluginsAsync(PluginListParameters parameters, CancellationToken cancellationToken = default)
        {
            IQueryString queryParameters = parameters == null ? null : new QueryString<PluginListParameters>(parameters);
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Get, "plugins", queryParameters, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<Plugin[]>(response.Body);
        }

        public Task PushPluginAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Post, $"plugins/{name}/push", cancellationToken);
        }

        public Task RemovePluginAsync(string name, PluginRemoveParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            IQueryString queryParameters = parameters == null ? null : new QueryString<PluginRemoveParameters>(parameters);
            return _client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Delete, $"plugins/{name}", queryParameters, cancellationToken);
        }

        public Task UpgradePluginAsync(string name, PluginUpgradeParameters parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Privileges == null)
            {
                throw new ArgumentNullException($"{nameof(parameters)}.{nameof(parameters.Privileges)}");
            }

            var query = new QueryString<PluginUpgradeParameters>(parameters);
            var data = new JsonRequestContent<IList<PluginPrivilege>>(parameters.Privileges, _client.JsonSerializer);
            return _client.MakeRequestAsync(new[] { NoSuchPluginHandler }, HttpMethod.Post, $"plugins/{name}/upgrade", query, data, cancellationToken);
        }
    }
}
