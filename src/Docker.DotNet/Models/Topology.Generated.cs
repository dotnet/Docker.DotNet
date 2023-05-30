using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Topology // (swarm.Topology)
    {
        [DataMember(Name = "Segments", EmitDefaultValue = false)]
        public IDictionary<string, string> Segments { get; set; }
    }
}
