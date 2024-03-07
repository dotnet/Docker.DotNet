using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerWaitResponse // (main.ContainerWaitResponse)
    {
        [JsonPropertyName("Error")]
        public WaitExitError Error { get; set; }

        [JsonPropertyName("StatusCode")]
        public long StatusCode { get; set; }
    }
}
