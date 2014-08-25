using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Docker.DotNet.Models;
using System.Threading;
using System.IO;

namespace Docker.DotNet
{
	public interface IContainerOperations
	{
		Task<IList<ContainerListResponse>> ListContainersAsync (ListContainersParameters parameters);

		Task<ContainerResponse> InspectContainerAsync (string id);

		Task<ContainerProcessesResponse> ListProcessesAsync (string id, ListProcessesParameters parameters);

		Task<IList<FilesystemChange>> InspectChangesAsync (string id);

		Task<Stream> ExportContainerAsync (string id, CancellationToken cancellationToken);

		Task<bool> StartContainerAsync (string id, HostConfig hostConfig);

		Task<bool> StopContainerAsync (string id, StopContainerParameters parameters, CancellationToken cancellationToken);

		Task RestartContainerAsync (string id, RestartContainerParameters parameters, CancellationToken cancellationToken);

		Task KillContainerAsync (string id, KillContainerParameters parameters);

		Task PauseContainerAsync (string id);

		Task UnpauseContainerAsync (string id);

		Task<WaitContainerResponse> WaitContainerAsync (string id, CancellationToken cancellationToken);

		Task RemoveContainerAsync (string id, RemoveContainerParameters parameters);

		Task<Stream> GetContainerLogsAsync (string id, GetContainerLogsParameters parameters, CancellationToken cancellationToken);

		Task<Stream> CopyFromContainerAsync (string id, CopyFromContainerParameters parameters, CancellationToken cancellationToken);
	}
}
