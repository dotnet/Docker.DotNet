using System;
using System.Net;

namespace Docker.DotNet
{
    public class DockerApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public string ResponseBody { get; }

        public DockerApiException(HttpStatusCode statusCode, string responseBody)
            : base($"Docker API responded with status code={statusCode}, response={responseBody}")
        {
            this.StatusCode = statusCode;
            this.ResponseBody = responseBody;
        }

        public DockerApiException() : base()
        {
        }

        protected DockerApiException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public DockerApiException(string message) : base(message)
        {
        }

        public DockerApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}