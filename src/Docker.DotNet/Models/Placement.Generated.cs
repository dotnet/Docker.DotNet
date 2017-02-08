using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Placement // (swarm.Placement)
    {
        [DataMember(Name = "Constraints", EmitDefaultValue = false)]
        public IList<string> Constraints { get; set; }
    }
}
