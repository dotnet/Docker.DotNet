using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Net.Http.Client
{
    public class ContentLengthReadStream : ApmStream
    {
        private readonly Stream _inner;
        private long _bytesRemaining;
        private bool _disposed;

        public ContentLengthReadStream(ApmStream inner, long contentLength)
        {
            _inner = inner;
            _bytesRemaining = contentLength;
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

        private void UpdateBytesRemaining(int read)
        {
            _bytesRemaining -= read;
            if (_bytesRemaining <= 0)
            {
                _disposed = true;
            }
            System.Diagnostics.Debug.Assert(_bytesRemaining >= 0, "Negative bytes remaining? " + _bytesRemaining);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // TODO: Validate buffer
            if (_disposed)
            {
                return 0;
            }
            int toRead = (int)Math.Min(count, _bytesRemaining);
            int read = _inner.Read(buffer, offset, toRead);
            UpdateBytesRemaining(read);
            return read;
        }

        public async override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            // TODO: Validate args
            if (_disposed)
            {
                return 0;
            }
            cancellationToken.ThrowIfCancellationRequested();
            int toRead = (int)Math.Min(count, _bytesRemaining);
            int read = await _inner.ReadAsync(buffer, offset, toRead, cancellationToken);
            UpdateBytesRemaining(read);
            return read;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>(state);
            InternalReadAsync(buffer, offset, count, callback, tcs);
            return tcs.Task;
        }
        private async void InternalReadAsync(byte[] buffer, int offset, int count, AsyncCallback callback, TaskCompletionSource<int> tcs)
        {
            try
            {
                int read = await ReadAsync(buffer, offset, count);
                tcs.TrySetResult(read);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            try
            {
                callback(tcs.Task);
            }
            catch (Exception)
            {
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            Task<int> t = (Task<int>)asyncResult;
            t.Wait();

            if (t.IsFaulted)
            {
                throw new IOException(string.Empty, t.Exception);
            }
            return t.Result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: Sync drain with timeout if small number of bytes remaining?  This will let us re-use the connection.
                _inner.Dispose();
            }
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

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException();
        }

        public override void EndWrite(IAsyncResult asyncResult)
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
