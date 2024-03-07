using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmUpdateConfig // (swarm.UpdateConfig)
    {
        [JsonPropertyName("Parallelism")]
        public ulong Parallelism { get; set; }

        [JsonPropertyName("Delay")]
        public long Delay { get; set; }

        [JsonPropertyName("FailureAction")]
        public string FailureAction { get; set; }

        [JsonPropertyName("Monitor")]
        public long Monitor { get; set; }

        [JsonPropertyName("MaxFailureRatio")]
        public float MaxFailureRatio { get; set; }

        [JsonPropertyName("Order")]
        public string Order { get; set; }
    }
}
