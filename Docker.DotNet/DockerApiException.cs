using System;
using System.Net;

namespace Docker.DotNet
{
    public class DockerApiException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public string ResponseBody { get; private set; }

        public DockerApiException(HttpStatusCode statusCode, string responseBody)
            : base(string.Format("Docker API responded with status code={0}, response={1}", statusCode, responseBody))
        {
            this.StatusCode = statusCode;
            this.ResponseBody = responseBody;
        }
    }
}