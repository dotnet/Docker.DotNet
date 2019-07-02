using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    internal static class StreamUtil
    {
        internal static async Task MonitorStreamAsync(Task<Stream> streamTask, DockerClient client, CancellationToken cancel, IProgress<string> progress)
        {
            using (var stream = await streamTask)
            {
                // ReadLineAsync must be cancelled by closing the whole stream.
                using (cancel.Register(() => stream.Dispose()))
                {
                    using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                    {
                        string line;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            progress.Report(line);
                        }
                    }
                }
            }
        }

        internal static async Task MonitorStreamForMessagesAsync<T>(Task<Stream> streamTask, DockerClient client, CancellationToken cancel, IProgress<T> progress)
        {
            using (var stream = await streamTask)
            {
                // ReadLineAsync must be cancelled by closing the whole stream.
                using (cancel.Register(() => stream.Dispose()))
                {
                    using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                    {
                        string line;
                        try
                        {
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                var prog = client.JsonSerializer.DeserializeObject<T>(line);
                                if (prog == null) continue;

                                progress.Report(prog);
                            }
                        }
                        catch (ObjectDisposedException)
                        {
                            // The subsequent call to reader.ReadLineAsync() after cancellation
                            // will fail because we disposed the stream. Just ignore here.
                        }
                    }
                }
            }
        }

        internal static async Task MonitorResponseForMessagesAsync<T>(Task<HttpResponseMessage> responseTask, DockerClient client, CancellationToken cancel, IProgress<T> progress)
        {
            using (var response = await responseTask)
            {
                await client.HandleIfErrorResponseAsync(response.StatusCode, response);

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    // ReadLineAsync must be cancelled by closing the whole stream.
                    using (cancel.Register(() => stream.Dispose()))
                    {
                        using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                        {
                            string line;
                            try
                            {
                                while ((line = await reader.ReadLineAsync()) != null)
                                {
                                    var prog = client.JsonSerializer.DeserializeObject<T>(line);
                                    if (prog == null) continue;

                                    progress.Report(prog);
                                }
                            }
                            catch (ObjectDisposedException)
                            {
                                // The subsequent call to reader.ReadLineAsync() after cancellation
                                // will fail because we disposed the stream. Just ignore here.
                            }
                        }
                    }
                }
            }
        }
    }
}