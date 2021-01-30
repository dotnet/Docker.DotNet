using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    public interface ISystemOperations
    {
        /// <summary>
        /// <para>Check auth configuration.</para>
        /// <para>Validate credentials for a registry and, if available, get an identity token for accessing the registry without password.</para>
        /// </summary>
        /// <remarks>
        /// 200 - An identity token was generated successfully.
        /// 204 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task AuthenticateAsync(AuthConfig authConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get system information.
        /// </summary>
        /// <remarks>
        /// <para>docker info</para>
        /// <para>
        /// 200 - No error.
        /// 500 - Server error.
        /// </para>
        /// </remarks>
        Task<SystemInfoResponse> GetSystemInfoAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Get version.</para>
        /// <para>Returns the version of Docker that is running and various information about the system that Docker is running on.</para>
        /// </summary>
        /// <remarks>
        /// <para>docker version</para>
        /// <para>
        /// 200 - No error.
        /// 500 - Server error.
        /// </para>
        /// </remarks>
        Task<VersionResponse> GetVersionAsync(CancellationToken cancellationToken = default);

        [Obsolete("Use 'Task MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken, IProgress<JSONMessage> progress)'")]
        Task<Stream> MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// <para>Monitor events.</para>
        /// <para>Stream real-time events from the server.</para>
        /// <para>Various objects within Docker report events when something happens to them.</para>
        /// <para>Containers report these events: {attach, commit, copy, create, destroy, detach, die, exec_create, exec_detach, exec_start, export, kill, oom, pause, rename, resize, restart, start, stop, top, unpause, update}</para>
        /// <para>Images report these events: {delete, import, load, pull, push, save, tag, untag}</para>
        /// <para>Volumes report these events: {create, mount, unmount, destroy}</para>
        /// <para>Networks report these events: {create, connect, disconnect, destroy}</para>
        /// <para>The Docker daemon reports these events: {reload}</para>
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task MonitorEventsAsync(ContainerEventsParameters parameters, IProgress<Message> progress, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Ping.</para>
        /// <para>This is a dummy endpoint you can use to test if the server is accessible.</para>
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task PingAsync(CancellationToken cancellationToken = default);
    }
}
