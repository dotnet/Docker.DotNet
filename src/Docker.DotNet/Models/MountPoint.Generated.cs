using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class MountPoint // (types.MountPoint)
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("Destination")]
        public string Destination { get; set; }

        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

        [JsonPropertyName("Mode")]
        public string Mode { get; set; }

        [JsonPropertyName("RW")]
        public bool RW { get; set; }

        [JsonPropertyName("Propagation")]
        public string Propagation { get; set; }
    }
}
