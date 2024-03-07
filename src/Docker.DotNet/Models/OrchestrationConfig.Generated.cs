using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class OrchestrationConfig // (swarm.OrchestrationConfig)
    {
        [JsonPropertyName("TaskHistoryRetentionLimit")]
        public long? TaskHistoryRetentionLimit { get; set; }
    }
}
