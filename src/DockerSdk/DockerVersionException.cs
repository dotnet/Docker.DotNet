using System;

namespace DockerSdk
{
    /// <summary>
    /// Indicates that API version negotiation failed because there is no overlap between the versions that the SDK
    /// supports with the versions the Docker daemon supports.
    /// </summary>
    [Serializable]
    public class DockerVersionException : DockerException
    {
        public DockerVersionException()
        {
        }

        public DockerVersionException(string message) : base(message)
        {
        }

        public DockerVersionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DockerVersionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
