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
        /// 200 - Summary image data for the images matching the query.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<ImagesListResponse>> ListImagesAsync(ImagesListParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inspect an image.
        /// 
        /// Return low-level information about an image.
        /// </summary>
        /// <remarks>
        /// docker inspect
        /// docker image inspect
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
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or id.</param>
        Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Tag an image.
        /// 
        /// Tag an image so that it becomes part of a registry.
        /// </summary>
        /// <remarks>
        /// docker tag
        /// docker image tag
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
        /// 200 - No error.
        /// 500 - Server error.
        /// </remarks>
        Task<IList<ImageSearchResponse>> SearchImagesAsync(ImagesSearchParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress)'")]
        Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig);

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

        [Obsolete("Use 'Task PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress)'")]
        Task<Stream> PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig);

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
        Task PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress)'")]
        Task<Stream> PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig);

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
        /// 200 - No error.
        /// 404 - No such image.
        /// 500 - Server error.
        /// </remarks>
        /// <param name="name">Image name or id.</param>
        Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));
    }
}