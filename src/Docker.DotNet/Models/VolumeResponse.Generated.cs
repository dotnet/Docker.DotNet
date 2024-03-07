using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VolumeResponse // (main.VolumeResponse)
    {
        [JsonPropertyName("ClusterVolume")]
        public ClusterVolume ClusterVolume { get; set; }

        [JsonPropertyName("CreatedAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("Mountpoint")]
        public string Mountpoint { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Options")]
        public IDictionary<string, string> Options { get; set; }

        [JsonPropertyName("Scope")]
        public string Scope { get; set; }

        [JsonPropertyName("Status")]
        public IDictionary<string, object> Status { get; set; }

        [JsonPropertyName("UsageData")]
        public UsageData UsageData { get; set; }
    }
}
