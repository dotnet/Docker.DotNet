using System;

namespace Docker.DotNet
{
    public interface IDockerClient : IDisposable
    {
        IConfigsOperations Configs { get; }
        DockerClientConfiguration Configuration { get; }

        IContainerOperations Containers { get; }
        TimeSpan DefaultTimeout { get; set; }
        IExecOperations Exec { get; }
        IImageOperations Images { get; }

        INetworkOperations Networks { get; }

        IPluginOperations Plugin { get; }
        ISecretsOperations Secrets { get; }
        ISwarmOperations Swarm { get; }
        ISystemOperations System { get; }
        ITasksOperations Tasks { get; }
        IVolumeOperations Volumes { get; }
    }
}
