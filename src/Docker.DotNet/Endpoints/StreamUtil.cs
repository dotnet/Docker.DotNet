
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet.Models
{
    internal static class StreamUtil
    {
        internal static async Task MonitorStream(Task<Stream> streamTask, DockerClient client, CancellationToken cancel, IProgress<string> progress)
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

        internal static async Task MonitorStreamForMessages<T>(Task<Stream> streamTask, DockerClient client, CancellationToken cancel, IProgress<T> progress)
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
                            var prog = client.JsonSerializer.DeserializeObject<T>(line);
                            if (prog == null) continue;

                            progress.Report(prog);
                        }
                    }
                }
            }
        }
    }
}