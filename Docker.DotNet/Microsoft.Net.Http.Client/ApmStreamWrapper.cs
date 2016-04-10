using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Net.Http.Client
{
    public class ApmStreamWrapper : ApmStream
    {
        private Stream _innerStream;

	    public ApmStreamWrapper(Stream innerStream)
	    {
            _innerStream = innerStream;
        }

        public override bool CanRead
        {
            get
            {
                return _innerStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _innerStream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return _innerStream.CanWrite;
            }
        }

        public override bool CanTimeout
        {
            get
            {
                return _innerStream.CanTimeout;
            }
        }

        public override int ReadTimeout
        {
            get
            {
                return _innerStream.ReadTimeout;
            }

            set
            {
                _innerStream.ReadTimeout = value;
            }
        }

        public override int WriteTimeout
        {
            get
            {
                return _innerStream.WriteTimeout;
            }

            set
            {
                _innerStream.WriteTimeout = value;
            }
        }

        public override long Length
        {
            get
            {
                return _innerStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return _innerStream.Position;
            }

            set
            {
                _innerStream.Position = value;
            }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
        {
#if DNXCORE50
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>(state);
            InternalReadAsync(buffer, offset, size, callback, tcs);
            return tcs.Task;
#else
            return _innerStream.BeginRead(buffer, offset, size, callback, state);
#endif
        }

#if DNXCORE50
        private async void InternalReadAsync(byte[] buffer, int offset, int size, AsyncCallback callback, TaskCompletionSource<int> tcs)
        {
            try
            {
                int read = await _innerStream.ReadAsync(buffer, offset, size);
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
#endif

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
        {
#if DNXCORE50
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(state);
            InternalWriteAsync(buffer, offset, size, callback, tcs);
            return tcs.Task;
#else
            return _innerStream.BeginWrite(buffer, offset, size, callback, state);
#endif
        }

#if DNXCORE50
        private async void InternalWriteAsync(byte[] buffer, int offset, int size, AsyncCallback callback, TaskCompletionSource<object> tcs)
        {
            try
            {
                await _innerStream.WriteAsync(buffer, offset, size);
                tcs.TrySetResult(null);
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
#endif

        public override int EndRead(IAsyncResult asyncResult)
        {
#if DNXCORE50
            Task<int> t = (Task<int>)asyncResult;
            t.Wait();

            if (t.IsFaulted)
            {
                throw new IOException(string.Empty, t.Exception);
            }
            return t.Result;
#else
            return _innerStream.EndRead(asyncResult);
#endif
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
#if DNXCORE50
            Task t = (Task)asyncResult;
            t.Wait();

            if (t.IsFaulted)
            {
                throw new IOException(string.Empty, t.Exception);
            }
#else
            _innerStream.EndWrite(asyncResult);
#endif
        }

        public override void Flush()
        {
            _innerStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _innerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _innerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerStream.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _innerStream.Dispose();
            }
        }

        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            return _innerStream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return _innerStream.FlushAsync(cancellationToken);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _innerStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _innerStream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            return _innerStream.ReadByte();
        }

        public override void WriteByte(byte value)
        {
            _innerStream.WriteByte(value);
        }
    }
}
