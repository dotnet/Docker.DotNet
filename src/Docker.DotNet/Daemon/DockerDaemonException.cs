using System;

namespace Docker.DotNet.Daemon
{
    public class DockerDaemonException : Exception
    {
        public DockerDaemonException(string message) : base($"error from daemon in stream: {message}")
        {
        }
    }
}
