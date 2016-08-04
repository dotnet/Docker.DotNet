using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    public interface IMiscellaneousOperations
    {
        Task AuthenticateAsync(AuthConfig authConfig);

        Task<VersionResponse> GetVersionAsync();

        Task PingAsync();

        Task<SystemInfoResponse> GetSystemInfoAsync();

        Task<Stream> MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken);

        Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters);

        Task<Stream> GetImageAsTarballAsync(string name, CancellationToken cancellationToken);

        Task<Stream> GetImagesAsTarballAsync(string[] names, CancellationToken cancellationToken);

        Task LoadImageFromTarballAsync(Stream stream, CancellationToken cancellationToken);

        Task<Stream> LoadImageFromTarball(Stream stream, ImageLoadParameters parameters, CancellationToken cancellationToken);

        Task<Stream> BuildImageFromDockerfileAsync(Stream contents, ImageBuildParameters parameters, CancellationToken cancellationToken);
    }
}