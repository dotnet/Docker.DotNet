using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace Docker.DotNet
{
    internal class DockerApiStreamedResponse
    {
        public DockerApiStreamedResponse(HttpStatusCode statusCode, Stream body, HttpResponseHeaders headers)
        {
            StatusCode = statusCode;
            Body = body;
            Headers = headers;
        }

        public Stream Body { get; private set; }
        public HttpResponseHeaders Headers { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
    }
}
