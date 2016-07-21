using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Docker.DotNet
{
    internal class BinaryRequestContent : IRequestContent
    {
        private Stream Stream { get; set; }

        private string MimeType { get; set; }

        public BinaryRequestContent(Stream stream, string mimeType)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (string.IsNullOrEmpty(mimeType))
            {
                throw new ArgumentNullException(nameof(mimeType));
            }

            this.Stream = stream;
            this.MimeType = mimeType;
        }

        public HttpContent GetContent()
        {
            StreamContent data = new StreamContent(this.Stream);
            data.Headers.ContentType = new MediaTypeHeaderValue(this.MimeType);
            return data;
        }
    }
}