using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    public interface IContainerOperations
    {
        Task<IList<ContainerListResponse>> ListContainersAsync(ContainersListParameters parameters);

        Task<ContainerInspectResponse> InspectContainerAsync(string id);

        Task<CreateContainerResponse> CreateContainerAsync(CreateContainerParameters parameters);

        Task<ContainerProcessesResponse> ListProcessesAsync(string id, ContainerListProcessesParameters parameters);

        Task<IList<ContainerFileSystemChangeResponse>> InspectChangesAsync(string id);

        Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken);

        Task<ContainerExecCreateResponse> ExecCreateContainerAsync(string id, ContainerExecCreateParameters parameters);

        Task<bool> StartContainerAsync(string id, HostConfig hostConfig);

        Task<bool> StopContainerAsync(string id, ContainerStopParameters parameters, CancellationToken cancellationToken);

        Task RestartContainerAsync(string id, ConatinerRestartParameters parameters, CancellationToken cancellationToken);

        Task KillContainerAsync(string id, ContainerKillParameters parameters);

        Task PauseContainerAsync(string id);

        Task UnpauseContainerAsync(string id);

        Task<ContainerWaitResponse> WaitContainerAsync(string id, CancellationToken cancellationToken);

        Task RemoveContainerAsync(string id, ContainerRemoveParameters parameters);

        Task<Stream> GetContainerLogsAsync(string id, ContainerLogsParameters parameters, CancellationToken cancellationToken);

        Task<GetArchiveFromContainerResponse> GetArchiveFromContainerAsync(string id, GetArchiveFromContainerParameters parameters, bool statOnly, CancellationToken cancellationToken);

        Task ExtractArchiveToContainerAsync(string id, ContainerPathStatParameters parameters, Stream stream, CancellationToken cancellationToken);

        Task<MultiplexedStream> AttachContainerAsync(string id, bool tty, ContainerAttachParameters parameters, CancellationToken cancellationToken);

        Task ResizeContainerTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken);

        Task StartContainerExecAsync(string id, CancellationToken cancellationToken);

        Task<MultiplexedStream> StartAndAttachContainerExecAsync(string id, bool tty, CancellationToken cancellationToken);
        
        Task<MultiplexedStream> StartWithConfigContainerExecAsync(string id, ExecConfig eConfig, CancellationToken cancellationToken);

        Task<ContainerExecInspectResponse> InspectContainerExecAsync(string id, CancellationToken cancellationToken);

        Task ResizeContainerExecTtyAsync(string id, ContainerResizeParameters parameters, CancellationToken cancellationToken);

        Task<Stream> GetContainerStatsAsync(string id, ContainerStatsParameters parameters, CancellationToken cancellationToken);
    }
}
