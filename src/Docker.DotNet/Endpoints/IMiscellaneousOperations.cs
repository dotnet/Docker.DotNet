using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System;

namespace Docker.DotNet
{
    public interface IMiscellaneousOperations
    {
        Task AuthenticateAsync(AuthConfig authConfig);

        Task<VersionResponse> GetVersionAsync();

        Task PingAsync();

        Task<SystemInfoResponse> GetSystemInfoAsync();

        [Obsolete("Use 'Task MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken, IProgress<EventsMessage> progress)'")]
        Task<Stream> MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken);

        Task MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken, IProgress<EventsMessage> progress);

        Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters);

        Task<Stream> GetImageAsTarballAsync(string name, CancellationToken cancellationToken);

        Task<Stream> GetImagesAsTarballAsync(string[] names, CancellationToken cancellationToken);

        Task LoadImageFromTarballAsync(Stream stream, CancellationToken cancellationToken);

        Task<Stream> LoadImageFromTarball(Stream stream, ImageLoadParameters parameters, CancellationToken cancellationToken);

        Task<Stream> BuildImageFromDockerfileAsync(Stream contents, ImageBuildParameters parameters, CancellationToken cancellationToken);
    }
}