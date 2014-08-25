using System;
using Docker.DotNet.Models;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Docker.DotNet
{
	public interface IMiscellaneousOperations
	{
		Task AuthenticateAsync (AuthConfig authConfig);

		Task<VersionResponse> GetVersionAsync ();

		Task PingAsync ();

		Task<SystemInfoResponse> GetSystemInfoAsync ();

		Task<Stream> MonitorEventsAsync (MonitorDockerEventsParameters parameters, CancellationToken cancellationToken);

		Task<CommitContainerChangesResponse> CommitContainerChangesAsync (CommitContainerChangesParameters parameters);

		Task<Stream> GetImageAsTarballAsync (string name, CancellationToken cancellationToken);

		Task LoadImageFromTarballAsync (Stream stream);

		Task<Stream> BuildImageFromDockerfileAsync (Stream contents, BuildImageFromDockerfileParameters parameters, CancellationToken cancellationToken);
	}
}

