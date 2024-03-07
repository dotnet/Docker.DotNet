using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerState // (types.ContainerState)
    {
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("Running")]
        public bool Running { get; set; }

        [JsonPropertyName("Paused")]
        public bool Paused { get; set; }

        [JsonPropertyName("Restarting")]
        public bool Restarting { get; set; }

        [JsonPropertyName("OOMKilled")]
        public bool OOMKilled { get; set; }

        [JsonPropertyName("Dead")]
        public bool Dead { get; set; }

        [JsonPropertyName("Pid")]
        public long Pid { get; set; }

        [JsonPropertyName("ExitCode")]
        public long ExitCode { get; set; }

        [JsonPropertyName("Error")]
        public string Error { get; set; }

        [JsonPropertyName("StartedAt")]
        public string StartedAt { get; set; }

        [JsonPropertyName("FinishedAt")]
        public string FinishedAt { get; set; }

        [JsonPropertyName("Health")]
        public Health Health { get; set; }
    }
}
