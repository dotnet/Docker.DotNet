using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ReplicatedService // (swarm.ReplicatedService)
    {
        [JsonPropertyName("Replicas")]
        public ulong? Replicas { get; set; }
    }
}
