using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VolumeAttachment // (swarm.VolumeAttachment)
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("Target")]
        public string Target { get; set; }
    }
}
