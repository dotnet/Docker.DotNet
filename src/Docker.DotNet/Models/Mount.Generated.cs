using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Mount // (mount.Mount)
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("Target")]
        public string Target { get; set; }

        [JsonPropertyName("ReadOnly")]
        public bool ReadOnly { get; set; }

        [JsonPropertyName("Consistency")]
        public string Consistency { get; set; }

        [JsonPropertyName("BindOptions")]
        public BindOptions BindOptions { get; set; }

        [JsonPropertyName("VolumeOptions")]
        public VolumeOptions VolumeOptions { get; set; }

        [JsonPropertyName("TmpfsOptions")]
        public TmpfsOptions TmpfsOptions { get; set; }

        [JsonPropertyName("ClusterOptions")]
        public ClusterOptions ClusterOptions { get; set; }
    }
}
