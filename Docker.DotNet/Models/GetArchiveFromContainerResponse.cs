using System.IO;

namespace Docker.DotNet.Models
{
    public class GetArchiveFromContainerResponse
    {
        public ContainerPathStats Stats { get; set; }

        public Stream Stream { get; set; }
    }
}
