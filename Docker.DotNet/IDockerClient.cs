using System;

namespace Docker.DotNet
{
    public interface IDockerClient : IDisposable
    {
        IImageOperations Images { get; }

        IContainerOperations Containers { get; }

        IMiscellaneousOperations Miscellaneous { get; }
    }
}
