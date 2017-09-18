using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet
{
    public interface IContainerOperations
    {
        /// <summary>
        /// List containers.
        /// </summary>
        /// <remarks>
        /// docker ps
        /// docker container ls
        ///
        /// 200 - No error.
        /// 400 - Bad parameter.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<ContainerListResponse>> ListContainersAsync(ContainersListParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inspect a container.
        ///
        /// Return low-level information about a container.
        /// </summary>
        /// <remarks>
        /// docker inspect
        /// docker container inspect
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<ContainerInspectResponse> InspectContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create a container
        /// </summary>
        /// <remarks>
        /// docker container create
        ///
        /// 201 - Container created successfully.
        /// 400 - Bad parameter.
        /// 404 - No such container.
        /// 406 - Impossible to attach.
        /// 409 - Conflict.
        /// 500 - Server error.
        /// </remarks>
        Task<CreateContainerResponse> CreateContainerAsync(CreateContainerParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List processes running inside a container.
        ///
        /// On Unix systems, this is done by running the {ps} command. The endpoint is not supported on Windows.
        /// </summary>
        /// <remarks>
        /// docker top
        /// docker container top
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<ContainerProcessesResponse> ListProcessesAsync(string id, ContainerListProcessesParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get changes on a container's filesystem.
        ///
        /// Returns which files in a container's filesystem have been added, deleted, or modified. The {Kind} of modification can be one of:
        ///     0: Modified
        ///     1: Added
        ///     2: Deleted
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<IList<ContainerFileSystemChangeResponse>> InspectChangesAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Export a container.
        ///
        /// Export the contents of a container as a tarball.
        /// </summary>
        /// <remarks>
        /// docker export
        /// docker container export
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create an exec instance.
        ///
        /// Runs a command inside a running container.
        /// </summary>
        /// <remarks>
        /// docker exec
        /// docker container exec
        ///
        /// 201 - No error.
        /// 404 - No such container.
        /// 409 - Container is paused.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<ContainerExecCreateResponse> ExecCreateContainerAsync(string id, ContainerExecCreateParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Start a container.
        /// </summary>
        /// <remarks>
        /// docker start
        /// docker container start
        ///
        /// 204 - No error.
        /// 304 - Container already started.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<bool> StartContainerAsync(string id, ContainerStartParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Stop a container.
        /// </summary>
        /// <remarks>
        /// docker stop
        /// docker container stop
        ///
        /// 204 - No error.
        /// 304 - Container already stopped.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<bool> StopContainerAsync(string id, ContainerStopParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Restart a container.
        /// </summary>
        /// <remarks>
        /// docker restart
        /// docker container restart
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task RestartContainerAsync(string id, ConatinerRestartParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Kill a container.
        ///
        /// Send a POSIX signal to a container, defaulting to killing to the container.
        /// </summary>
        /// <remarks>
        /// docker kill
        /// docker container kill
        ///
        /// 204 - No error.
        /// 304 - Container already started.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task KillContainerAsync(string id, ContainerKillParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Pause a container.
        ///
        /// Uses the cgroups freezer to suspend all processes in a container.
        ///
        /// Traditionally, when suspending a process the {SIGSTOP} signal is used, which is observable by the process being suspended.
        /// With the cgroups freezer the process is unaware, and unable to capture, that it is being suspended, and subsequently resumed.
        /// </summary>
        /// <remarks>
        /// docker pause
        /// docker container pause
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task PauseContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Unpause a container.
        ///
        /// Resume a container which has been paused.
        /// </summary>
        /// <remarks>
        /// docker unpause
        /// docker container unpause
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task UnpauseContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Wait for a container.
        ///
        /// Block until a container stops, then returns the exit code.
        /// </summary>
        /// <remarks>
        /// docker wait
        /// docker container wait
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<ContainerWaitResponse> WaitContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove a container.
        /// </summary>
        /// <remarks>
        /// docker rm
        /// docker container rm
        ///
        /// 204 - No error.
        /// 400 - Bad parameter.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task RemoveContainerAsync(string id, ContainerRemoveParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get container logs.
        ///
        /// Get {stdout} and {stderr} logs from a container.
        /// Note: This endpoint works only for containers with the {json-file} or {journald} logging driver.
        /// </summary>
        /// <remarks>
        /// docker logs
        /// docker container logs
        ///
        /// 101 - Logs returned as a stream.
        /// 200 - Logs returned as a string in response body.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<Stream> GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken, IProgress<string> progress)'")]
        Task GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken, IProgress<string> progress);

        /// <summary>
        /// Get information about files in a container
        ///
        /// -OR-
        ///
        /// Get an archive of a filestream resource in a container.
        /// </summary>
        /// <remarks>
        /// 204 - No error.
        /// 400 - Bad parameter.
        /// 404 - Container or path does not exist.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        /// <param name="statOnly">If <c>True</c> will only return file information. If <c>False</c> will return the tarball stream.</param>
        Task<GetArchiveFromContainerResponse> GetArchiveFromContainerAsync(string id, GetArchiveFromContainerParameters parameters, bool statOnly, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Extract an archive of files or folders to a directory in a container.
        ///
        /// Upload a tar archive to be extracted to a path in the filesystem of container id.
        /// </summary>
        /// <remarks>
        /// 204 - The content was extracted successfully.
        /// 400 - Bad parameter.
        /// 403 - Permission denied, the volume or container rootfs is marked as read-only.
        /// 404 - No such container or path does not exist inside the container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task ExtractArchiveToContainerAsync(string id, ContainerPathStatParameters parameters, Stream stream, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Attach to a container.
        /// </summary>
        /// <remarks>
        /// docker attach
        /// docker container attach
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        /// <param name="tty">Is this a TTY stream.</param>
        Task<MultiplexedStream> AttachContainerAsync(string id, bool tty, ContainerAttachParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resize a container TTY.
        ///
        /// Resize the TTY for a container. You must restart the container for the size to take effect.
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task ResizeContainerTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Start an exec instance.
        ///
        /// Starts a previously set up exec instance. If detach is true, this endpoint returns immediately after starting
        /// the command. Otherwise, it sets up an interactive session with the command.
        /// </summary>
        /// <remarks>
        /// 204 - No error.
        /// 404 - No such exec instance.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">Exec instance ID.</param>
        Task StartContainerExecAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<MultiplexedStream> StartAndAttachContainerExecAsync(string id, bool tty, CancellationToken cancellationToken = default(CancellationToken));

        Task<MultiplexedStream> StartWithConfigContainerExecAsync(string id, ExecConfig eConfig, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inspect an exec instance.
        ///
        /// Return low-level information about an exec instance.
        /// </summary>
        /// <remarks>
        /// docker inspect
        ///
        /// 200 - No error.
        /// 404 - No such exec instance.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">Exec instance ID.</param>
        Task<ContainerExecInspectResponse> InspectContainerExecAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resize an exec instance.
        ///
        /// Resize the TTY session used by an exec instance. This endpoint only works if {tty} was specified as part of
        /// creating and starting the exec instance.
        /// </summary>
        /// <remarks>
        /// 201 - No error.
        /// 404 - No such exec instance.
        /// </remarks>
        /// <param name="id">Exec instance ID.</param>
        Task ResizeContainerExecTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task GetContainerStatsAsync(string id, ContainerStatsParameters parameters, CancellationToken cancellationToken, IProgress<JSONMessage> progress)'")]
        Task<Stream> GetContainerStatsAsync(string id, ContainerStatsParameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Get container stats based on resource usage.
        ///
        /// This endpoint returns a live stream of a container's resource usage statistics.
        /// The {precpu_stats} is the CPU statistic of last read, which is used for calculating the CPU usage percentage. It is not the same as the
        /// {cpu_stats} field.
        /// </summary>
        /// <remarks>
        /// docker stats
        /// docker container stats
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task GetContainerStatsAsync(string id, ContainerStatsParameters parameters, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Rename a container
        /// </summary>
        /// <remarks>
        /// POST /containers/{id}/rename
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 409 - Name already in use.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        /// <param name="parameters">New name of the container.</param>
        Task RenameContainerAsync(string id, ContainerRenameParameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Delete stopped containers
        /// </summary>
        /// <remarks>
        /// HTTP POST /containers/prune
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<ContainersPruneResponse> PruneContainersAsync(ContainersPruneParameters parameters = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
