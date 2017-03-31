using System;

namespace Docker.DotNet
{
    public interface IDockerClient : IDisposable
    {
        IImageOperations Images { get; }

        IContainerOperations Containers { get; }

        ISystemOperations System { get; }

        INetworkOperations Networks { get; }

        ISwarmOperations Swarm { get; }
    }
}
