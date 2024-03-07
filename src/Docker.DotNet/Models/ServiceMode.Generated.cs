using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ServiceMode // (swarm.ServiceMode)
    {
        [JsonPropertyName("Replicated")]
        public ReplicatedService Replicated { get; set; }

        [JsonPropertyName("Global")]
        public GlobalService Global { get; set; }

        [JsonPropertyName("ReplicatedJob")]
        public ReplicatedJob ReplicatedJob { get; set; }

        [JsonPropertyName("GlobalJob")]
        public GlobalJob GlobalJob { get; set; }
    }
}
