using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Net.Http.Client
{
    public class BufferedReadStream : Stream
    {
        private const char CR = '\r';
        private const char LF = '\n';

        private readonly Stream _inner;
        private readonly byte[] _buffer;
        private int _bufferOffset = 0;
        private int _bufferCount = 0;
        private bool _disposed;

        public BufferedReadStream(Stream inner)
        {
            if (inner == null)
            {
                throw new ArgumentNullException("inner");
            }
            _inner = inner;
            _buffer = new byte[1024];
        }

        public override bool CanRead
        {
            get { return _inner.CanRead || _bufferCount > 0; }
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
            get { return _inner.CanWrite; }
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

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            _disposed = true;
            if (disposing)
            {
                _inner.Dispose();
            }
        }

        public override void Flush()
        {
            _inner.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return _inner.FlushAsync(cancellationToken);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _inner.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _inner.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Validate Inputs

            // Drain buffer
            if (_bufferCount > 0)
            {
                int toCopy = Math.Min(_bufferCount, count);
                Buffer.BlockCopy(_buffer, _bufferOffset, buffer, offset, toCopy);
                _bufferOffset += toCopy;
                _bufferCount -= toCopy;
                return toCopy;
            }

            return _inner.Read(buffer, offset, count);
        }

        public async override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            // Validate Inputs

            // Drain buffer
            if (_bufferCount > 0)
            {
                int toCopy = Math.Min(_bufferCount, count);
                Buffer.BlockCopy(_buffer, _bufferOffset, buffer, offset, toCopy);
                _bufferOffset += toCopy;
                _bufferCount -= toCopy;
                return toCopy;
            }

            return await _inner.ReadAsync(buffer, offset, count, cancellationToken);
        }

        private void EnsureBufferd()
        {
            if (_bufferCount == 0)
            {
                _bufferOffset = 0;
                _bufferCount = _inner.Read(_buffer, _bufferOffset, _buffer.Length);
                if (_bufferCount == 0)
                {
                    throw new IOException("Unexpected end of stream");
                }
            }
        }

        private async Task EnsureBufferdAsync(CancellationToken cancel)
        {
            if (_bufferCount == 0)
            {
                _bufferOffset = 0;
                _bufferCount = await _inner.ReadAsync(_buffer, _bufferOffset, _buffer.Length, cancel);
                if (_bufferCount == 0)
                {
                    throw new IOException("Unexpected end of stream");
                }
            }
        }

        // TODO: Line length limits?
        public string ReadLine()
        {
            CheckDisposed();
            StringBuilder builder = new StringBuilder();
            bool foundCR = false, foundCRLF = false;
            do
            {
                if (_bufferCount == 0)
                {
                    EnsureBufferd();
                }
                char ch = (char)_buffer[_bufferOffset]; // TODO: Encoding enforcement
                builder.Append(ch);
                _bufferOffset++;
                _bufferCount--;
                if (ch == CR)
                {
                    foundCR = true;
                }
                else if (ch == LF)
                {
                    if (foundCR)
                    {
                        foundCRLF = true;
                    }
                    else
                    {
                        foundCR = false;
                    }
                }
            }
            while (!foundCRLF);

            return builder.ToString(0, builder.Length - 2); // Drop the CRLF
        }

        // TODO: Line length limits?
        public async Task<string> ReadLineAsync(CancellationToken cancel)
        {
            CheckDisposed();
            StringBuilder builder = new StringBuilder();
            bool foundCR = false, foundCRLF = false;
            do
            {
                if (_bufferCount == 0)
                {
                    await EnsureBufferdAsync(cancel);
                }
                char ch = (char)_buffer[_bufferOffset]; // TODO: Encoding enforcement
                builder.Append(ch);
                _bufferOffset++;
                _bufferCount--;
                if (ch == CR)
                {
                    foundCR = true;
                }
                else if (ch == LF)
                {
                    if (foundCR)
                    {
                        foundCRLF = true;
                    }
                    else
                    {
                        foundCR = false;                             
                    }
                }
            }
            while (!foundCRLF);

            return builder.ToString(0, builder.Length - 2); // Drop the CRLF
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(typeof(BufferedReadStream).FullName);
            }
        }
    }
}
