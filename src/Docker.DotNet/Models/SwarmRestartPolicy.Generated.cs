using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmRestartPolicy // (swarm.RestartPolicy)
    {
        [JsonPropertyName("Condition")]
        public string Condition { get; set; }

        [JsonPropertyName("Delay")]
        public long? Delay { get; set; }

        [JsonPropertyName("MaxAttempts")]
        public ulong? MaxAttempts { get; set; }

        [JsonPropertyName("Window")]
        public long? Window { get; set; }
    }
}
