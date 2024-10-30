using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Daemon;
using Xunit;

namespace Docker.DotNet.Tests;

public class MultiplexedStreamTests
{
    [Fact]
    public async Task WriteToDifferentStreams_CopyOutputToAsync_DividedStreams()
    {
        using var source = new MemoryStream();
        using var stdout = new MemoryStream();
        using var stderr = new MemoryStream();
        using var stream = new MultiplexedStream(source, multiplexed: true);

        await WriteMessage(source, MultiplexedStream.TargetStream.StandardOut, "information");
        await WriteMessage(source, MultiplexedStream.TargetStream.StandardError, "some error");

        source.Seek(0, SeekOrigin.Begin);
        await stream.CopyOutputToAsync(Stream.Null, stdout, stderr, CancellationToken.None);

        stdout.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(stdout))
            Assert.Equal("information", await reader.ReadToEndAsync());

        stderr.Seek(0, SeekOrigin.Begin);
        using (var reader = new StreamReader(stderr))
            Assert.Equal("some error", await reader.ReadToEndAsync());
    }

    [Fact]
    public async Task WriteToSystemError_CopyOutputToAsync_ThrowAnExceptionWithReceivedMessage()
    {
        using var source = new MemoryStream();
        using var stream = new MultiplexedStream(source, multiplexed: true);

        await WriteMessage(source, MultiplexedStream.TargetStream.SystemError, "failed to grab logs");

        source.Seek(0, SeekOrigin.Begin);
        var exception = Assert.Throws<DockerDaemonException>(() =>
        {
            stream
                .CopyOutputToAsync(Stream.Null, Stream.Null, Stream.Null, CancellationToken.None)
                .GetAwaiter()
                .GetResult();
        });

        Assert.Contains("failed to grab logs", exception.Message);
    }

    private static async Task WriteHeaderAsync(Stream stream, MultiplexedStream.TargetStream target, int length)
    {
        var header = new byte[8];
        header[0] = (byte)target;
        header[4] = (byte)(length >> 24 & 0xFF);
        header[5] = (byte)(length >> 16 & 0xFF);
        header[6] = (byte)(length >> 8 & 0xFF);
        header[7] = (byte)(length >> 0 & 0xFF);
        await stream.WriteAsync(header);
    }

    private static async Task WriteMessage(Stream stream, MultiplexedStream.TargetStream target, string message)
    {
        var messageBody = Encoding.UTF8.GetBytes(message);
        await WriteHeaderAsync(stream, target, messageBody.Length);
        await stream.WriteAsync(messageBody);
    }
}
