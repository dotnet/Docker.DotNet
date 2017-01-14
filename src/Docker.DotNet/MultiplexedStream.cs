using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Net.Http.Client;

#if !NET45
using System.Buffers;
#endif

namespace Docker.DotNet
{
    public class MultiplexedStream : IDisposable
    {
        private readonly WriteClosableStream _stream;
        private TargetStream _target;
        private int _remaining;
        private readonly byte[] _header = new byte[8];
        private readonly bool _multiplexed;

        const int BufferSize = 81920;

        public MultiplexedStream(WriteClosableStream stream, bool multiplexed)
        {
            _stream = stream;
            _multiplexed = multiplexed;
        }

        public enum TargetStream
        {
            StandardIn = 0,
            StandardOut = 1,
            StandardError = 2
        }

        public struct ReadResult
        {
            public int Count { get; set; }
            public TargetStream Target { get; set; }
            public bool EOF => Count == 0;
        }

        public void CloseWrite()
        {
            _stream.CloseWrite();
        }

        public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public async Task<ReadResult> ReadOutputAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (!_multiplexed)
            {
                return new ReadResult
                {
                    Count = await _stream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false),
                    Target = TargetStream.StandardOut
                };
            }

            while (_remaining == 0)
            {
                for (var i = 0; i < _header.Length;)
                {
                    var n = await _stream.ReadAsync(_header, i, _header.Length - i, cancellationToken).ConfigureAwait(false);
                    if (n == 0)
                    {
                        if (i == 0)
                        {
                            // End of the stream.
                            return new ReadResult();
                        }

                        throw new EndOfStreamException();
                    }

                    i += n;
                }

                switch ((TargetStream)_header[0])
                {
                    case TargetStream.StandardIn:
                    case TargetStream.StandardOut:
                    case TargetStream.StandardError:
                        _target = (TargetStream)_header[0];
                        break;

                    default:
                        throw new IOException("unknown stream type");
                }

                _remaining = (_header[4] << 24) |
                            (_header[5] << 16) |
                            (_header[6] << 8) |
                            _header[7];
            }

            var toRead = Math.Min(count, _remaining);
            var read = await _stream.ReadAsync(buffer, offset, toRead, cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                throw new EndOfStreamException();
            }

            _remaining -= read;
            return new ReadResult
            {
                Count = read,
                Target = _target
            };
        }

        public async Task CopyFromAsync(Stream input, CancellationToken cancellationToken)
        {
#if !NET45
            var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
#else
            var buffer = new byte[BufferSize];
#endif

            try
            {
                for (;;)
                {
                    var count = await input.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                    if (count == 0)
                    {
                        break;
                    }

                    await WriteAsync(buffer, 0, count, cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
#if !NET45
                ArrayPool<byte>.Shared.Return(buffer);
#endif
            }
        }

        public async Task CopyOutputToAsync(Stream stdin, Stream stdout, Stream stderr, CancellationToken cancellationToken)
        {
#if !NET45
            var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
#else
            var buffer = new byte[BufferSize];
#endif

            try
            {
                for (;;)
                {
                    var result = await ReadOutputAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                    if (result.EOF)
                    {
                        return;
                    }

                    Stream stream;
                    switch (result.Target)
                    {
                        case TargetStream.StandardIn:
                            stream = stdin;
                            break;
                        case TargetStream.StandardOut:
                            stream = stdout;
                            break;
                        case TargetStream.StandardError:
                            stream = stderr;
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown TargetStream: '{result.Target}'.");
                    }

                    await stream.WriteAsync(buffer, 0, result.Count, cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
#if !NET45
                ArrayPool<byte>.Shared.Return(buffer);
#endif
            }
        }

        public void Dispose()
        {
            ((IDisposable)_stream).Dispose();
        }
    }
}