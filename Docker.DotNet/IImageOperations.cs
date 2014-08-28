using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    public interface IImageOperations
    {
        Task<IList<ImageListResponse>> ListImagesAsync(ListImagesParameters parameters);

        Task<ImageResponse> InspectImageAsync(string name);

        Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name);

        Task TagImageAsync(string name, TagImageParameters parameters);

        Task<IList<IDictionary<string, string>>> DeleteImageAsync(string name, DeleteImageParameters parameters);

        Task<IList<ImageSearchResponse>> SearchImagesAsync(SearchImagesParameters parameters);

        Task<Stream> CreateImageAsync(CreateImageParameters parameters, AuthConfig authConfig);

        Task<Stream> PushImageAsync(string name, PushImageParameters parameters, AuthConfig authConfig);
    }
}