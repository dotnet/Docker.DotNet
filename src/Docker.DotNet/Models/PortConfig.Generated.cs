using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PortConfig // (swarm.PortConfig)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Protocol")]
        public string Protocol { get; set; }

        [JsonPropertyName("TargetPort")]
        public uint TargetPort { get; set; }

        [JsonPropertyName("PublishedPort")]
        public uint PublishedPort { get; set; }

        [JsonPropertyName("PublishMode")]
        public string PublishMode { get; set; }
    }
}
