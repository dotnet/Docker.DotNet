namespace Docker.DotNet
{
    public interface IDockerClient
    {
        IImageOperations Images { get; }

        IContainerOperations Containers { get; }

        IMiscellaneousOperations Miscellaneous { get; }
    }
}
