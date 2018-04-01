using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System;
using System.IO;
using System.Threading;

namespace Docker.DotNet
{
    public interface IPluginOperations
    {
        /// <summary>
        /// List plugins.
        /// </summary>
        /// <remarks>
        /// docker plugin ls
        ///
        /// HTTP GET /plugins
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<Plugin>> ListPluginsAsync(PluginListParameters parameters, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Install a plugin.
        /// </summary>
        /// <remarks>
        /// docker plugin pull
        ///
        /// HTTP POST /plugins/pull
        ///
        /// 204 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task InstallPluginAsync(PluginInstallParameters parameters, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inspect a plugin.
        /// </summary>
        /// <remarks>
        /// docker plugin inspect
        ///
        /// HTTP GET /plugins/{name}/json
        ///
        /// 200 - No error.
        /// 404 - plugin is not installed.
        /// 500 - Server error.
        /// </remarks>
        Task<Plugin> InspectPluginAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove a plugin.
        /// </summary>
        /// <remarks>
        /// docker plugin rm
        ///
        /// HTTP DELETE /plugins/{name}
        ///
        /// 200 - No error.
        /// 404 - plugin is not installed.
        /// 500 - Server error.
        /// </remarks>
        Task RemovePluginAsync(string name, PluginRemoveParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Enable a plugin.
        /// </summary>
        /// <remarks>
        /// docker plugin enable
        ///
        /// HTTP POST /plugins/{name}/enable
        ///
        /// 200 - No error.
        /// 404 - plugin is not installed.
        /// 500 - Server error.
        /// </remarks>
        Task EnablePluginAsync(string name, PluginEnableParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Disable a plugin.
        /// </summary>
        /// <remarks>
        /// docker plugin disable
        ///
        /// HTTP POST /plugins/{name}/disable
        ///
        /// 200 - No error.
        /// 404 - plugin is not installed.
        /// 500 - Server error.
        /// </remarks>
        Task DisablePluginAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get plugin privileges.
        /// </summary>
        /// <remarks>
        /// docker plugin privileges
        ///
        /// HTTP POST /plugins/{name}/disable
        ///
        /// 200 - No error.
        /// 404 - plugin is not installed.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<PluginPrivilegesParameters>> GetPluginPrivilegesAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    }
}
