using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System;
using System.IO;
using System.Threading;

namespace Docker.DotNet
{
    public interface IImageOperations
    {
        /// <summary>
        /// List Images.
        /// 
        /// Returns a list of images on the server. Not that it uses a different, smaller representation
        /// of an image than inspecting a single image.
        /// </summary>
        /// <remarks>
        /// docker images
        /// docker image ls
        ///
        /// HTTP GET /images/json
        ///
        /// 200 - Summary image data for the images matching the query.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<ImagesListResponse>> ListImagesAsync(ImagesListParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

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
        /// HTTP POST /build
        ///
        /// 204 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<Stream> BuildImageFromDockerfileAsync(Stream contents, ImageBuildParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create an image.
        /// 
        /// Create an image by either pulling it from a registry or importing it.
        /// </summary>
        /// <remarks>
        /// docker pull
        /// docker image pull
        ///
        /// 200 - No error.
        /// 404 - Repository does not exist or no read access.
        /// 500 - Server error.
        /// </remarks>
        Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create an image.
        /// 
        /// Create an image by either pulling it from a registry or importing it.
        /// </summary>
        /// <remarks>
        /// docker pull
        /// docker image pull
        ///
        /// 200 - No error.
        /// 404 - Repository does not exist or no read access.
        /// 500 - Server error.
        /// </remarks>
        Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IDictionary<string, string> headers, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create an image.
        /// 
        /// Create an image by either pulling it from a registry or importing it.
        /// </summary>
        /// <remarks>
        /// docker pull
        /// docker image pull
        /// docker import
        /// 
        /// HTTP POST /images/create
        ///
        /// 200 - No error.
        /// 404 - Repository does not exist or no read access.
        /// 500 - Server error.
        /// </remarks>
        Task CreateImageAsync(ImagesCreateParameters parameters, Stream imageStream, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Create an image.
        /// 
        /// Create an image by either pulling it from a registry or importing it.
        /// </summary>
        /// <remarks>
        /// docker pull
        /// docker image pull
        /// docker import
        /// 
        /// HTTP POST /images/create
        ///
        /// 200 - No error.
        /// 404 - Repository does not exist or no read access.
        /// 500 - Server error.
        /// </remarks>
        Task CreateImageAsync(ImagesCreateParameters parameters, Stream imageStream, AuthConfig authConfig, IDictionary<string, string> headers, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inspect an image.
        /// 
        /// Return low-level information about an image.
        /// </summary>
        /// <remarks>
        /// docker inspect
        /// docker image inspect
        ///
        /// HTTP GET /images/{name}/json
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or id.</param>
        Task<ImageInspectResponse> InspectImageAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get the history of an image.
        /// 
        /// Return parent layers of an image.
        /// </summary>
        /// <remarks>
        /// docker history
        /// docker image history
        ///
        /// HTTP GET /images/{name}/history
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or id.</param>
        Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Push an image.
        /// 
        /// Push an image to a registry.
        /// 
        /// If you wish to push an image on to a private registry, that image must already have a tag which
        /// references that registry. For example {registry.example.com/myimage:latest}.
        /// 
        /// The push is cancelled if the HTTP connection is closed.
        /// </summary>
        /// <remarks>
        /// docker push
        /// docker image push
        ///
        /// HTTP POST /images/{name}/push
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or id.</param>
        Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Tag an image.
        /// 
        /// Tag an image so that it becomes part of a registry.
        /// </summary>
        /// <remarks>
        /// docker tag
        /// docker image tag
        ///
        /// HTTP POST /images/{name}/tag
        ///
        /// 201 - No error.
        /// 400 - Bad parameter.
        /// 404 - No such image.
        /// 409 - Conflict.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or id.</param>
        Task TagImageAsync(string name, ImageTagParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove an image.
        /// 
        /// Remove an image, along with any untagged parent images that were referenced by that image.
        /// 
        /// Images can't be removed if they have descendant images, are being used by a running container
        /// or are being used by a build.
        /// </summary>
        /// <remarks>
        /// docker inspect
        /// docker image inspect
        ///
        /// HTTP DELETE /images/{name}
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or id.</param>
        Task<IList<IDictionary<string, string>>> DeleteImageAsync(string name, ImageDeleteParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Search images
        /// 
        /// Search for an image on Docker Hub.
        /// </summary>
        /// <remarks>
        /// docker search
        ///
        /// HTTP GET /images/search
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<ImageSearchResponse>> SearchImagesAsync(ImagesSearchParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete unused images
        /// </summary>
        /// <remarks>
        /// docker rmi
        ///
        /// HTTP POST /images/prune
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<ImagesPruneResponse> PruneImagesAsync(ImagesPruneParameters parameters = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create a new image from a container.
        /// </summary>
        /// <remarks>
        /// docker commit
        ///
        /// HTTP POST /commit
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
        /// HTTP GET /images/{name}/get
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or ID.</param>
        Task<Stream> SaveImageAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

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
        /// HTTP GET /images/get
        ///
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="names">Image names to filter by.</param>
        Task<Stream> SaveImagesAsync(string[] names, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Loads a set of images and tags into a Docker repository.
        /// </summary>
        /// <remarks>
        /// docker load
        ///
        /// HTTP POST /images/load
        ///
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task LoadImageAsync(ImageLoadParameters parameters, Stream imageStream, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));
    }
}