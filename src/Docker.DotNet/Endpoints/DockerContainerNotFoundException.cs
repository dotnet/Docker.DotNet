using System.Net;

namespace Docker.DotNet
{
    public class DockerContainerNotFoundException : DockerApiException
    {
        public DockerContainerNotFoundException(HttpStatusCode statusCode, string responseBody) : base(statusCode, responseBody)
        {
        }

        public DockerContainerNotFoundException() : base()
        {
        }

        public DockerContainerNotFoundException(string message) : base(message)
        {
        }

        public DockerContainerNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected DockerContainerNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
