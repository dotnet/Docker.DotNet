using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System;

namespace Docker.DotNet
{
    public interface IMiscellaneousOperations
    {
        Task AuthenticateAsync(AuthConfig authConfig, CancellationToken cancellationToken = default(CancellationToken));

        Task<VersionResponse> GetVersionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task PingAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<SystemInfoResponse> GetSystemInfoAsync(CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken, IProgress<JSONMessage> progress)'")]
        Task<Stream> MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken);

        Task MonitorEventsAsync(ContainerEventsParameters parameters, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<Stream> GetImageAsTarballAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<Stream> GetImagesAsTarballAsync(string[] names, CancellationToken cancellationToken = default(CancellationToken));

        Task LoadImageFromTarballAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken));

        Task<Stream> LoadImageFromTarball(Stream stream, ImageLoadParameters parameters = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<Stream> BuildImageFromDockerfileAsync(Stream contents, ImageBuildParameters parameters, CancellationToken cancellationToken = default(CancellationToken));
    }
}