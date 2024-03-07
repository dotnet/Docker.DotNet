using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class TaskStatus // (swarm.TaskStatus)
    {
        [JsonPropertyName("Timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("State")]
        public TaskState State { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }

        [JsonPropertyName("Err")]
        public string Err { get; set; }

        [JsonPropertyName("ContainerStatus")]
        public ContainerStatus ContainerStatus { get; set; }

        [JsonPropertyName("PortStatus")]
        public PortStatus PortStatus { get; set; }
    }
}
