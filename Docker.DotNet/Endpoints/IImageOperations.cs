using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Docker.DotNet.Models;

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

        Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig);

        Task<Stream> PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig);

        Task<Stream> PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig);
    }
}