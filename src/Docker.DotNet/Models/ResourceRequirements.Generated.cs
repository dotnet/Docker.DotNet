using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ResourceRequirements // (swarm.ResourceRequirements)
    {
        [JsonPropertyName("Limits")]
        public SwarmLimit Limits { get; set; }

        [JsonPropertyName("Reservations")]
        public SwarmResources Reservations { get; set; }
    }
}
