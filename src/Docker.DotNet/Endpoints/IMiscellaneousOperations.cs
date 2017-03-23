using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System;

namespace Docker.DotNet
{
    public interface IMiscellaneousOperations
    {
        /// <summary>
        /// Check auth configuration.
        /// 
        /// Validate credentials for a registry and, if available, get an identity token for accessing the registry without password.
        /// </summary>
        /// <remarks>
        /// 200 - An identity token was generated successfully.
        /// 204 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task AuthenticateAsync(AuthConfig authConfig, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get version.
        /// 
        /// Returns the version of Docker that is running and various information about the system that Docker is running on.
        /// </summary>
        /// <remarks>
        /// docker version
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<VersionResponse> GetVersionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Ping.
        /// 
        /// This is a dummy endpoint you can use to test if the server is accessible.
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task PingAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get system information.
        /// </summary>
        /// <remarks>
        /// docker info
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<SystemInfoResponse> GetSystemInfoAsync(CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken, IProgress<JSONMessage> progress)'")]
        Task<Stream> MonitorEventsAsync(ContainerEventsParameters parameters, CancellationToken cancellationToken);

        /// <summary>
        /// Monitor events.
        /// 
        /// Stream real-time events from the server.
        ///
        /// Various objects within Docker report events when something happens to them.
        ///
        /// Containers report these events: {attach, commit, copy, create, destroy, detach, die, exec_create, exec_detach, exec_start, export, kill, oom, pause, rename, resize, restart, start, stop, top, unpause, update}
        ///
        /// Images report these events: {delete, import, load, pull, push, save, tag, untag}
        ///
        /// Volumes report these events: {create, mount, unmount, destroy}
        ///
        /// Networks report these events: {create, connect, disconnect, destroy}
        ///
        /// The Docker daemon reports these events: {reload}
        /// </summary>
        /// <remarks>
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task MonitorEventsAsync(ContainerEventsParameters parameters, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create a new image from a container.
        /// </summary>
        /// <remarks>
        /// docker commit
        ///
        /// 201 - No error.
        /// 404 - No such container.
        /// 500 - Server error.
        /// </remarks>
        Task<CommitContainerChangesResponse> CommitContainerChangesAsync(CommitContainerChangesParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Export an image.
        /// 
        /// Get a tarball containing all images and metadata for a repository.
        /// </summary>
        /// <remarks>
        /// docker export
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or ID.</param>
        Task<Stream> GetImageAsTarballAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Export several images.
        /// 
        /// Get a tarball containing all images and metadata for several image repositories.
        /// 
        /// For each value of the {names} parameter: if it is a specific name and tag(e.g. {ubuntu:latest}),
        /// then only that image(and its parents) are returned; if it is an image ID, similarly only that
        /// image (and its parents) are returned and there would be no names referenced in the 'repositories'
        /// file for this image ID.
        ///
        /// For details on the format, see the {export image endpoint}.
        /// </summary>
        /// <remarks>
        /// docker export
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="names">Image names to filter by.</param>
        Task<Stream> GetImagesAsTarballAsync(string[] names, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Import images
        ///
        /// Load a set of images and tags into a repository.
        /// 
        ///For details on the format, see {the export image endpoint}.
        /// </summary>
        /// <remarks>
        /// docker import
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task LoadImageFromTarballAsync(Stream stream, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Import images
        ///
        /// Load a set of images and tags into a repository.
        /// 
        ///For details on the format, see {the export image endpoint}.
        /// </summary>
        /// <remarks>
        /// docker import
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<Stream> LoadImageFromTarball(Stream stream, ImageLoadParameters parameters = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Build an image.
        /// 
        /// Build an image from a tar archive with a {Dockerfile} in it.
        /// 
        /// The {Dockerfile } specifies how the image is built from the tar archive. It is typically in the
        /// archive's root, but can be at a different path or have a different name by specifying the {dockerfile}
        /// parameter. See the Dockerfile reference for more information.
        /// 
        /// The Docker daemon performs a preliminary validation of the {Dockerfile} before starting the build,
        /// and returns an error if the syntax is incorrect. After that, each instruction is run one-by-one until
        /// the ID of the new image is output.
        /// 
        /// The build is canceled if the client drops the connection by quitting or being killed.
        /// </summary>
        /// <remarks>
        /// docker build
        /// docker image build
        ///
        /// 204 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<Stream> BuildImageFromDockerfileAsync(Stream contents, ImageBuildParameters parameters, CancellationToken cancellationToken = default(CancellationToken));
    }
}