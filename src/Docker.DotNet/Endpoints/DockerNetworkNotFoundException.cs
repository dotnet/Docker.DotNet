using System.Net;

namespace Docker.DotNet
{
    public class DockerNetworkNotFoundException : DockerApiException
    {
        public DockerNetworkNotFoundException(HttpStatusCode statusCode, string responseBody) : base(statusCode, responseBody)
        {
        }

        public DockerNetworkNotFoundException() : base()
        {
        }

        public DockerNetworkNotFoundException(string message) : base(message)
        {
        }

        public DockerNetworkNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected DockerNetworkNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}