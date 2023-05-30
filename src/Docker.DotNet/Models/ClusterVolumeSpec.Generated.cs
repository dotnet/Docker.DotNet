using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ClusterVolumeSpec // (volume.ClusterVolumeSpec)
    {
        [DataMember(Name = "Group", EmitDefaultValue = false)]
        public string Group { get; set; }

        [DataMember(Name = "AccessMode", EmitDefaultValue = false)]
        public AccessMode AccessMode { get; set; }

        [DataMember(Name = "AccessibilityRequirements", EmitDefaultValue = false)]
        public TopologyRequirement AccessibilityRequirements { get; set; }

        [DataMember(Name = "CapacityRange", EmitDefaultValue = false)]
        public CapacityRange CapacityRange { get; set; }

        [DataMember(Name = "Secrets", EmitDefaultValue = false)]
        public IList<VolumeSecret> Secrets { get; set; }

        [DataMember(Name = "Availability", EmitDefaultValue = false)]
        public string Availability { get; set; }
    }
}
