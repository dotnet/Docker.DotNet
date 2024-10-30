using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainersPruneResponse // (types.ContainersPruneReport)
    {
        [JsonPropertyName("ContainersDeleted")]
        public IList<string> ContainersDeleted { get; set; }

        [JsonPropertyName("SpaceReclaimed")]
        public ulong SpaceReclaimed { get; set; }
    }
}
