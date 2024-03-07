using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class TaskDefaults // (swarm.TaskDefaults)
    {
        [JsonPropertyName("LogDriver")]
        public SwarmDriver LogDriver { get; set; }
    }
}
