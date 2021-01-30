using System.Net;

namespace Docker.DotNet
{
    public class DockerImageNotFoundException : DockerApiException
    {
        public DockerImageNotFoundException(HttpStatusCode statusCode, string body) : base(statusCode, body)
        {
        }

        public DockerImageNotFoundException() : base()
        {
        }

        public DockerImageNotFoundException(string message) : base(message)
        {
        }

        public DockerImageNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected DockerImageNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
