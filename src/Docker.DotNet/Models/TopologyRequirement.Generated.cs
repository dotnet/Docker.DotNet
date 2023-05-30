using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class TopologyRequirement // (volume.TopologyRequirement)
    {
        [DataMember(Name = "Requisite", EmitDefaultValue = false)]
        public IList<VolumeTopology> Requisite { get; set; }

        [DataMember(Name = "Preferred", EmitDefaultValue = false)]
        public IList<VolumeTopology> Preferred { get; set; }
    }
}
