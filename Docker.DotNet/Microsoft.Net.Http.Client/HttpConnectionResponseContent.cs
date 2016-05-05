using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Net.Http.Client
{
    internal class HttpConnectionResponseContent : HttpContent
    {
        private readonly HttpConnection _connection;
        private Stream _responseStream;

        public HttpConnectionResponseContent(HttpConnection connection)
        {
            _connection = connection;
        }

        public void ResolveResponseStream(bool chunked)
        {
            if (_responseStream != null)
            {
                throw new InvalidOperationException("Called multiple times");
            }
            if (chunked)
            {
                _responseStream = new ChunkedReadStream(_connection.Transport);
            }
            else if (Headers.ContentLength.HasValue)
            {
                _responseStream = new ContentLengthReadStream(_connection.Transport, Headers.ContentLength.Value);
            }
            else
            {
                // Raw, read until end and close
                _responseStream = _connection.Transport;
            }
        }

        protected override Task SerializeToStreamAsync(Stream stream, System.Net.TransportContext context)
        {
            return _responseStream.CopyToAsync(stream);
        }

        protected override Task<Stream> CreateContentReadStreamAsync()
        {
            return Task.FromResult(_responseStream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            _responseStream.Dispose();
        }
    }
}
