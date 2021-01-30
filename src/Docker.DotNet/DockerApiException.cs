using System;
using System.Net;

namespace Docker.DotNet
{
    public class DockerApiException : Exception
    {
        public DockerApiException(HttpStatusCode statusCode, string responseBody)
            : base($"Docker API responded with status code={statusCode}, response={responseBody}")
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }

        public string ResponseBody { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
