using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmResources // (swarm.Resources)
    {
        [JsonPropertyName("NanoCPUs")]
        public long NanoCPUs { get; set; }

        [JsonPropertyName("MemoryBytes")]
        public long MemoryBytes { get; set; }

        [JsonPropertyName("GenericResources")]
        public IList<GenericResource> GenericResources { get; set; }
    }
}
