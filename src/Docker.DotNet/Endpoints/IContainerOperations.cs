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
        /// HTTP GET /containers/json
        ///
        /// 200 - No error.
        /// 400 - Bad parameter.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<ContainerListResponse>> ListContainersAsync(ContainersListParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create a container
        /// </summary>
        /// <remarks>
        /// docker container create
        ///
        /// HTTP POST /containers/create
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
        /// Inspect a container.
        ///
        /// Return low-level information about a container.
        /// </summary>
        /// <remarks>
        /// docker inspect
        /// docker container inspect
        ///
        /// HTTP GET /containers/(id)/json
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<ContainerInspectResponse> InspectContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List processes running inside a container.
        ///
        /// On Unix systems, this is done by running the {ps} command. The endpoint is not supported on Windows.
        /// </summary>
        /// <remarks>
        /// docker top
        /// docker container top
        ///
        /// HTTP GET /containers/(id)/top
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<ContainerProcessesResponse> ListProcessesAsync(string id, ContainerListProcessesParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

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
        /// HTTP GET /containers/(id)/logs
        ///
        /// 101 - Logs returned as a stream.
        /// 200 - Logs returned as a string in response body.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        [Obsolete("The stream returned by this method won't be demultiplexed properly if the container was created without a TTY. Use GetContainerLogsAsync(string, bool, ContainerLogsParameters, CancellationToken) instead")]
        Task<Stream> GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the logs from a container.
        /// This method is only suited for containers created with a TTY. For containers created without a TTY, use
        /// <see cref="GetContainerLogsAsync(string, bool, ContainerLogsParameters, CancellationToken)"/> instead.
        /// </summary>
        /// <param name="id">ID or name of the container.</param>
        /// <param name="parameters">The parameters used to retrieve the logs.</param>
        /// <param name="cancellationToken">A token used to cancel this operation.</param>
        /// <param name="progress">
        /// The class that will receive the log lines.
        /// Every reported string represents one log line, with its terminating newline removed.
        /// </param>
        /// <returns>A Task that will complete once all log lines have been read, or once the container has exited if Follow is set to <see langword="true"/>.</returns>
        Task GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken, IProgress<string> progress);

        /// <summary>
        /// Gets the <code>stdout</code> and <code>stderr</code> logs from a container.
        /// This endpoint works only for containers with the <code>json-file</code> or <code>journald</code> logging driver.
        /// </summary>
        /// <param name="id">ID or name of the container.</param>
        /// <param name="tty">If the container was created with a TTY or not. If <see langword="false" />, the returned stream is multiplexed.</param>
        /// <param name="parameters">The parameters used to retrieve the logs.</param>
        /// <param name="cancellationToken">A token used to cancel this operation.</param>
        /// <returns>A stream with the retrieved logs. If the container wasn't created with a TTY, this stream is multiplexed.</returns>
        Task<MultiplexedStream> GetContainerLogsAsync(string id, bool tty, ContainerLogsParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get changes on a container's filesystem.
        ///
        /// Returns which files in a container's filesystem have been added, deleted, or modified. The {Kind} of modification can be one of:
        ///     0: Modified
        ///     1: Added
        ///     2: Deleted
        /// </summary>
        /// <remarks>
        /// HTTP GET /containers/(id)/changes
        ///
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
        /// HTTP GET /containers/(id)/export
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

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
        /// HTTP GET /containers/(id)/stats
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task GetContainerStatsAsync(string id, ContainerStatsParameters parameters, IProgress<ContainerStatsResponse> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resize a container TTY.
        ///
        /// Resize the TTY for a container. You must restart the container for the size to take effect.
        /// </summary>
        /// <remarks>
        /// HTTP POST /containers/(id)/resize
        ///
        /// 200 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task ResizeContainerTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Start a container.
        /// </summary>
        /// <remarks>
        /// docker start
        /// docker container start
        ///
        /// HTTP POST /containers/(id)/start
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
        /// HTTP POST /containers/(id)/stop
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
        /// HTTP POST /containers/(id)/restart
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task RestartContainerAsync(string id, ContainerRestartParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Kill a container.
        ///
        /// Send a POSIX signal to a container, defaulting to killing to the container.
        /// </summary>
        /// <remarks>
        /// docker kill
        /// docker container kill
        ///
        /// HTTP POST /containers/(id)/kill
        ///
        /// 204 - No error.
        /// 304 - Container already started.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task KillContainerAsync(string id, ContainerKillParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Rename a container
        /// </summary>
        /// <remarks>
        /// HTTP POST /containers/{id}/rename
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
        /// HTTP POST /containers/(id)/pause
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
        /// HTTP POST /containers/(id)/unpause
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task UnpauseContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Attach to a container.
        /// </summary>
        /// <remarks>
        /// docker attach
        /// docker container attach
        ///
        /// HTTP POST /containers/(id)/attach
        ///
        /// 204 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        /// <param name="tty">Is this a TTY stream.</param>
        Task<MultiplexedStream> AttachContainerAsync(string id, bool tty, ContainerAttachParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        // TODO: Attach Web Socket

        /// <summary>
        /// Wait for a container.
        ///
        /// Block until a container stops, then returns the exit code.
        /// </summary>
        /// <remarks>
        /// docker wait
        /// docker container wait
        ///
        /// HTTP POST /containers/(id)/wait
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
        /// HTTP DELETE /containers/(id)
        ///
        /// 204 - No error.
        /// 400 - Bad parameter.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task RemoveContainerAsync(string id, ContainerRemoveParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get information about files in a container
        ///
        /// -OR-
        ///
        /// Get an archive of a filestream resource in a container.
        /// </summary>
        /// <remarks>
        /// HTTP HEAD /containers/(id)/archive
        /// HTTP GET /containers/(id)/archive
        ///
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
        /// HTTP PUT /containers/(id)/archive
        ///
        /// 204 - The content was extracted successfully.
        /// 400 - Bad parameter.
        /// 403 - Permission denied, the volume or container rootfs is marked as read-only.
        /// 404 - No such container or path does not exist inside the container.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task ExtractArchiveToContainerAsync(string id, ContainerPathStatParameters parameters, Stream stream, CancellationToken cancellationToken = default(CancellationToken));

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

        /// <summary>
        /// Update configuration of a container.
        /// </summary>
        /// <remarks>
        /// docker update
        /// 
        /// HTTP POST /containers/(id)/update
        /// 
        /// 200 - No error.
        /// 400 - Bad parameter.
        /// 404 - No such container.
        /// 500 - Server error
        /// </remarks>
        /// <param name="id">ID or name of the container.</param>
        Task<ContainerUpdateResponse> UpdateContainerAsync(string id, ContainerUpdateParameters parameters, CancellationToken cancellationToken = default(CancellationToken));
    }
}
