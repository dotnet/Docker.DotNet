using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerStatus // (swarm.ContainerStatus)
    {
        [JsonPropertyName("ContainerID")]
        public string ContainerID { get; set; }

        [JsonPropertyName("PID")]
        public long PID { get; set; }

        [JsonPropertyName("ExitCode")]
        public long ExitCode { get; set; }
    }
}
