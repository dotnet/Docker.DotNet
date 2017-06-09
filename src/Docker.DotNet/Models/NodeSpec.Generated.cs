using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NodeSpec // (swarm.NodeSpec)
    {
        public NodeSpec()
        {
        }

        public NodeSpec(Annotations Annotations)
        {
            if (Annotations != null)
            {
                this.Name = Annotations.Name;
                this.Labels = Annotations.Labels;
            }
        }

        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "Labels", EmitDefaultValue = false)]
        public IDictionary<string, string> Labels { get; set; }

        [DataMember(Name = "Role", EmitDefaultValue = false)]
        public string Role { get; set; }

        [DataMember(Name = "Availability", EmitDefaultValue = false)]
        public string Availability { get; set; }
    }
}
