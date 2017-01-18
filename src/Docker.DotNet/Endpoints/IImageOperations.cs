using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using System;

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

        Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress = null);

        Task PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress = null);

        Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<ImageOperationProgress> progress = null);
    }
}