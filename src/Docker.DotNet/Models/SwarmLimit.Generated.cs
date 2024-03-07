using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmLimit // (swarm.Limit)
    {
        [JsonPropertyName("NanoCPUs")]
        public long NanoCPUs { get; set; }

        [JsonPropertyName("MemoryBytes")]
        public long MemoryBytes { get; set; }

        [JsonPropertyName("Pids")]
        public long Pids { get; set; }
    }
}
