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
        Task<IList<ContainerListResponse>> ListContainersAsync(ContainersListParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<ContainerInspectResponse> InspectContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<CreateContainerResponse> CreateContainerAsync(CreateContainerParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<ContainerProcessesResponse> ListProcessesAsync(string id, ContainerListProcessesParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<ContainerFileSystemChangeResponse>> InspectChangesAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<ContainerExecCreateResponse> ExecCreateContainerAsync(string id, ContainerExecCreateParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> StartContainerAsync(string id, ContainerStartParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> StopContainerAsync(string id, ContainerStopParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task RestartContainerAsync(string id, ConatinerRestartParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task KillContainerAsync(string id, ContainerKillParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task PauseContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task UnpauseContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<ContainerWaitResponse> WaitContainerAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveContainerAsync(string id, ContainerRemoveParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<Stream> GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken, IProgress<string> progress)'")]
        Task GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken, IProgress<string> progress);

        Task<GetArchiveFromContainerResponse> GetArchiveFromContainerAsync(string id, GetArchiveFromContainerParameters parameters, bool statOnly, CancellationToken cancellationToken = default(CancellationToken));

        Task ExtractArchiveToContainerAsync(string id, ContainerPathStatParameters parameters, Stream stream, CancellationToken cancellationToken = default(CancellationToken));

        Task<MultiplexedStream> AttachContainerAsync(string id, bool tty, ContainerAttachParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task ResizeContainerTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task StartContainerExecAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<MultiplexedStream> StartAndAttachContainerExecAsync(string id, bool tty, CancellationToken cancellationToken = default(CancellationToken));

        Task<MultiplexedStream> StartWithConfigContainerExecAsync(string id, ExecConfig eConfig, CancellationToken cancellationToken = default(CancellationToken));

        Task<ContainerExecInspectResponse> InspectContainerExecAsync(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task ResizeContainerExecTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task GetContainerStatsAsync(string id, ContainerStatsParameters parameters, CancellationToken cancellationToken, IProgress<JSONMessage> progress)'")]
        Task<Stream> GetContainerStatsAsync(string id, ContainerStatsParameters parameters, CancellationToken cancellationToken);

        Task GetContainerStatsAsync(string id, ContainerStatsParameters parameters, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));
    }
}
