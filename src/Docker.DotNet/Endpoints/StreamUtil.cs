using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Docker.DotNet.Models
{
    internal static class StreamUtil
    {
        internal static async Task<Stream> MonitorResponseForMessagesAsync<T>(Task<HttpResponseMessage> responseTask, DockerClient client, CancellationToken cancel, IProgress<T> progress)
        {
            using (var response = await responseTask)
            {
                await DockerClient.HandleIfErrorResponseAsync(response.StatusCode, response);

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
                            return stream;
                        }
                    }
                }
            }
        }

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
                        while ((line = await reader.ReadLineAsync().WithCancellation(cancel)) != null)
                        //while ((line = await reader.ReadLineAsync()) != null)
                        {
                            progress.Report(line);
                        }
                    }
                }
            }
        }

        internal static async Task MonitorStreamForMessagesAsync<T>(Task<Stream> streamTask, DockerClient client, CancellationToken cancel, IProgress<T> progress)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();

            using (var stream = await streamTask)
            using (cancel.Register(() => stream.Dispose()))
            using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
            using (var jsonReader = new JsonTextReader(reader) { SupportMultipleContent = true })
            {
                try
                {
                    while (jsonReader.Read())
                    {
                        var ev = serializer.Deserialize<T>(jsonReader);
                        if (progress is null) continue;

                        progress.Report(ev);

                        if (cancel.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                }
                catch (AggregateException ex)
                {
                    foreach (var innerException in ex.InnerExceptions)
                    {
                        if (innerException is ObjectDisposedException)
                        {
                            // Ignore reads on disposed streams
                        }
                        else if (innerException is System.Net.Sockets.SocketException)
                        {
                            // Ignore reads on disposed streams
                        }
                        else if (innerException is System.IO.IOException)
                        {
                            // Ignore reads on disposed streams
                        }
                        else
                        {
                            throw innerException;
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Ignore reads on disposed streams
                }
            }
        }

        private static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }

            return await task;
        }
    }
}
