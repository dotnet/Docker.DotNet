using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerExecInspectResponse // (types.ContainerExecInspect)
    {
        [JsonPropertyName("ID")]
        public string ExecID { get; set; }

        [JsonPropertyName("ContainerID")]
        public string ContainerID { get; set; }

        [JsonPropertyName("Running")]
        public bool Running { get; set; }

        [JsonPropertyName("ExitCode")]
        public long ExitCode { get; set; }

        [JsonPropertyName("Pid")]
        public long Pid { get; set; }
    }
}
