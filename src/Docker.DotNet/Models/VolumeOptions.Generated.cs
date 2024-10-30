using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VolumeOptions // (mount.VolumeOptions)
    {
        [JsonPropertyName("NoCopy")]
        public bool NoCopy { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("DriverConfig")]
        public Driver DriverConfig { get; set; }
    }
}
