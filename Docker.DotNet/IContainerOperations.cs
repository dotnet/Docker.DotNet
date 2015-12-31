using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    public interface IContainerOperations
    {
        Task<IList<ContainerListResponse>> ListContainersAsync(ListContainersParameters parameters);

        Task<ContainerResponse> InspectContainerAsync(string id);

        Task<CreateContainerResponse> CreateContainerAsync(CreateContainerParameters parameters);

        Task<ContainerProcessesResponse> ListProcessesAsync(string id, ListProcessesParameters parameters);

        Task<IList<FilesystemChange>> InspectChangesAsync(string id);

        Task<Stream> ExportContainerAsync(string id, CancellationToken cancellationToken);

        Task<ExecCreateContainerResponse> ExecCreateContainerAsync(string id, ExecCreateContainerParameters parameters);

        Task<bool> StartContainerAsync(string id, HostConfig hostConfig);

        Task<bool> StopContainerAsync(string id, StopContainerParameters parameters, CancellationToken cancellationToken);

        Task RestartContainerAsync(string id, RestartContainerParameters parameters, CancellationToken cancellationToken);

        Task KillContainerAsync(string id, KillContainerParameters parameters);

        Task PauseContainerAsync(string id);

        Task UnpauseContainerAsync(string id);

        Task<WaitContainerResponse> WaitContainerAsync(string id, CancellationToken cancellationToken);

        Task RemoveContainerAsync(string id, RemoveContainerParameters parameters);

        Task<Stream> GetContainerLogsAsync(string id, GetContainerLogsParameters parameters, CancellationToken cancellationToken);

        Task<Stream> CopyFromContainerAsync(string id, CopyFromContainerParameters parameters, CancellationToken cancellationToken);

        Task<GetArchiveFromContainerResponse> GetArchiveFromContainerAsync(string id, GetArchiveFromContainerParameters parameters, bool statOnly, CancellationToken cancellationToken);

        Task ExtractArchiveToContainerAsync(string id, ExtractArchiveToContainerParameters parameters, Stream stream, CancellationToken cancellationToken);
    }
}
