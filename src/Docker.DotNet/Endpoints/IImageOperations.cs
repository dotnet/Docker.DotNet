using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System;
using System.IO;

namespace Docker.DotNet
{
    public interface IImageOperations
    {
        Task<IList<ImagesListResponse>> ListImagesAsync(ImagesListParameters parameters);

        Task<ImageInspectResponse> InspectImageAsync(string name);

        Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name);

        Task TagImageAsync(string name, ImageTagParameters parameters);

        Task<IList<IDictionary<string, string>>> DeleteImageAsync(string name, ImageDeleteParameters parameters);

        Task<IList<ImageSearchResponse>> SearchImagesAsync(ImagesSearchParameters parameters);

        [Obsolete("Use 'Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress)'")]
        Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig);

        Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress);

        [Obsolete("Use 'Task PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress)'")]
        Task<Stream> PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig);

        Task PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress);

        [Obsolete("Use 'Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress)'")]
        Task<Stream> PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig);

        Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress);
    }
}