using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VolumeInfo // (volume.Info)
    {
        [JsonPropertyName("CapacityBytes")]
        public long CapacityBytes { get; set; }

        [JsonPropertyName("VolumeContext")]
        public IDictionary<string, string> VolumeContext { get; set; }

        [JsonPropertyName("VolumeID")]
        public string VolumeID { get; set; }

        [JsonPropertyName("AccessibleTopology")]
        public IList<VolumeTopology> AccessibleTopology { get; set; }
    }
}
