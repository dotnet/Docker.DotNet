using System.Net;

namespace Docker.DotNet
{
    public class DockerSwarmException : DockerApiException 
    {
        public string DeserializedMessage { get; private set; }

        public DockerSwarmException(HttpStatusCode statusCode, string responseBody, string deserializedMessage) : base(statusCode, responseBody)
        {
            DeserializedMessage = deserializedMessage;
        }
    }
}
