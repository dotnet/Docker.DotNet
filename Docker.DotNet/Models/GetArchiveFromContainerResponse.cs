using System.IO;

namespace Docker.DotNet.Models
{
    public class GetArchiveFromContainerResponse
    {
        public ContainerPathStat Stat { get; set; }

        public Stream Stream { get; set; }
    }
}
