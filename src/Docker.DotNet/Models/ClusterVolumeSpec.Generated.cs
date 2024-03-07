using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ClusterVolumeSpec // (volume.ClusterVolumeSpec)
    {
        [JsonPropertyName("Group")]
        public string Group { get; set; }

        [JsonPropertyName("AccessMode")]
        public AccessMode AccessMode { get; set; }

        [JsonPropertyName("AccessibilityRequirements")]
        public TopologyRequirement AccessibilityRequirements { get; set; }

        [JsonPropertyName("CapacityRange")]
        public CapacityRange CapacityRange { get; set; }

        [JsonPropertyName("Secrets")]
        public IList<VolumeSecret> Secrets { get; set; }

        [JsonPropertyName("Availability")]
        public string Availability { get; set; }
    }
}
