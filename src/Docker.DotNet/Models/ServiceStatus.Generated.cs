using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ServiceStatus // (swarm.ServiceStatus)
    {
        [JsonPropertyName("RunningTasks")]
        public ulong RunningTasks { get; set; }

        [JsonPropertyName("DesiredTasks")]
        public ulong DesiredTasks { get; set; }

        [JsonPropertyName("CompletedTasks")]
        public ulong CompletedTasks { get; set; }
    }
}
