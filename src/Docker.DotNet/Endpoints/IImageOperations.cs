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
        Task<IList<ImagesListResponse>> ListImagesAsync(ImagesListParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<ImageInspectResponse> InspectImageAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<ImageHistoryResponse>> GetImageHistoryAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        Task TagImageAsync(string name, ImageTagParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<IDictionary<string, string>>> DeleteImageAsync(string name, ImageDeleteParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task<IList<ImageSearchResponse>> SearchImagesAsync(ImagesSearchParameters parameters, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress)'")]
        Task<Stream> CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig);

        Task CreateImageAsync(ImagesCreateParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress)'")]
        Task<Stream> PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig);

        Task PullImageAsync(ImagesPullParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use 'Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress)'")]
        Task<Stream> PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig);

        Task PushImageAsync(string name, ImagePushParameters parameters, AuthConfig authConfig, IProgress<JSONMessage> progress, CancellationToken cancellationToken = default(CancellationToken));

        Task ImportImageAsync(ImagesImportParameters parameters, AuthConfig authConfig, CancellationToken cancellationToken = default(CancellationToken));

        Task ImportImageAsync(ImagesImportParameters parameters, Stream imageStream, AuthConfig authConfig, CancellationToken cancellationToken = default(CancellationToken));
    }
}