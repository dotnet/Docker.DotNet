using System.IO;

namespace Docker.DotNet.SSH
{
    internal class JoinedReadWriteStream : Stream
    {
        private Stream _readStream;
        private Stream _writeStream;

        public JoinedReadWriteStream(Stream read, Stream write)
        {
            _readStream = read;
            _writeStream = write;
        }

        public override bool CanRead => _readStream.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => _writeStream.CanWrite;

#region Not implemented methods

        public override long Length => throw new System.NotImplementedException();

        public override long Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }

#endregion

        public override void Flush()
        {
            _writeStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _readStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _writeStream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            try
            {
                _writeStream.Close();
            }
            finally
            {
                _readStream.Close();
            }

            base.Close();
        }

        public override int GetHashCode()
        {
            return _readStream.GetHashCode() ^ _writeStream.GetHashCode();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    _writeStream.Dispose();
                }
                finally
                {
                    _readStream.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
