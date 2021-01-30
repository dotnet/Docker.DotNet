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

        public DockerApiException() : base()
        {
        }

        public DockerApiException(string message) : base(message)
        {
        }

        public DockerApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DockerApiException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public string ResponseBody { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
