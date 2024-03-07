using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ReplicatedJob // (swarm.ReplicatedJob)
    {
        [JsonPropertyName("MaxConcurrent")]
        public ulong? MaxConcurrent { get; set; }

        [JsonPropertyName("TotalCompletions")]
        public ulong? TotalCompletions { get; set; }
    }
}
