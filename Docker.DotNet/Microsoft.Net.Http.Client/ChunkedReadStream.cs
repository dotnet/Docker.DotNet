using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Net.Http.Client
{
    public class ChunkedReadStream : Stream
    {
        private readonly BufferedReadStream _inner;
        private bool _expectChunkHeader = true;
        private long _chunkBytesRemaining;
        private bool _disposed;

        public ChunkedReadStream(BufferedReadStream inner)
        {
            _inner = inner;
        }

        public override bool CanRead
        {
            get { return !_disposed; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanTimeout
        {
            get { return _inner.CanTimeout; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override int ReadTimeout
        {
            get
            {
                CheckDisposed();
                return _inner.ReadTimeout;
            }
            set
            {
                CheckDisposed();
                _inner.ReadTimeout = value;
            }
        }

        public override int WriteTimeout
        {
            get
            {
                CheckDisposed();
                return _inner.WriteTimeout;
            }
            set
            {
                CheckDisposed();
                _inner.WriteTimeout = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // TODO: Validate buffer
            if (_disposed)
            {
                return 0;
            }

            try
            {
                if (_expectChunkHeader)
                {
                    System.Diagnostics.Debug.Assert(_chunkBytesRemaining == 0);
                    string headerLine = _inner.ReadLine();
                    if (!long.TryParse(headerLine, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _chunkBytesRemaining))
                    {
                        throw new IOException("Invalid chunk header: " + headerLine);
                    }
                    _expectChunkHeader = false;
                }

                int read = 0;
                if (_chunkBytesRemaining > 0)
                {
                    // Not the last (empty) chunk.
                    int toRead = (int)Math.Min(count, _chunkBytesRemaining);
                    read = _inner.Read(buffer, offset, toRead);

                    _chunkBytesRemaining -= read;
                    System.Diagnostics.Debug.Assert(_chunkBytesRemaining >= 0, "Negative bytes remaining? " + _chunkBytesRemaining);
                }
                // TODO: else, drain trailer headers.

                if (_chunkBytesRemaining == 0)
                {
                    // End of chunk, read the terminator CRLF
                    string trailerLine = _inner.ReadLine();
                    System.Diagnostics.Debug.Assert(string.IsNullOrEmpty(trailerLine));
                    _expectChunkHeader = true;
                }

                if (read == 0)
                {
                    Dispose();
                }
                return read;
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public async override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            // TODO: Validate buffer
            if (_disposed)
            {
                return 0;
            }

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_expectChunkHeader)
                {
                    System.Diagnostics.Debug.Assert(_chunkBytesRemaining == 0);
                    string headerLine = await _inner.ReadLineAsync(cancellationToken);
                    if (!long.TryParse(headerLine, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _chunkBytesRemaining))
                    {
                        throw new IOException("Invalid chunk header: " + headerLine);
                    }
                    _expectChunkHeader = false;
                }

                int read = 0;
                if (_chunkBytesRemaining > 0)
                {
                    // Not the last (empty) chunk.
                    int toRead = (int)Math.Min(count, _chunkBytesRemaining);
                    read = await _inner.ReadAsync(buffer, offset, toRead, cancellationToken);

                    _chunkBytesRemaining -= read;
                    System.Diagnostics.Debug.Assert(_chunkBytesRemaining >= 0, "Negative bytes remaining? " + _chunkBytesRemaining);
                }
                // TODO: else, drain trailer headers.

                if (_chunkBytesRemaining == 0)
                {
                    // End of chunk, read the terminator CRLF
                    string trailerLine = await _inner.ReadLineAsync(cancellationToken);
                    System.Diagnostics.Debug.Assert(string.IsNullOrEmpty(trailerLine));
                    _expectChunkHeader = true;
                }

                if (read == 0)
                {
                    Dispose();
                }
                return read;
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: Sync drain with timeout if small number of bytes remaining?  This will let us re-use the connection.
                _inner.Dispose();
            }
            _disposed = true;
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(typeof(ContentLengthReadStream).FullName);
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }
    }
}
