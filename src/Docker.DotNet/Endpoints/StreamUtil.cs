
using System;
using System.IO;
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
                        while ((line = await reader.ReadLineAsync().WithCancellation(cancel)) != null)
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
                            while ((line = await reader.ReadLineAsync().WithCancellation(cancel)) != null)
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