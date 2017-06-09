using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SwarmInspectResponse // (swarm.Swarm)
    {
        public SwarmInspectResponse()
        {
        }

        public SwarmInspectResponse(ClusterInfo ClusterInfo)
        {
            if (ClusterInfo != null)
            {
                this.ID = ClusterInfo.ID;
                this.Version = ClusterInfo.Version;
                this.CreatedAt = ClusterInfo.CreatedAt;
                this.UpdatedAt = ClusterInfo.UpdatedAt;
                this.Spec = ClusterInfo.Spec;
            }
        }

        [DataMember(Name = "ID", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "Version", EmitDefaultValue = false)]
        public Version Version { get; set; }

        [DataMember(Name = "CreatedAt", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "UpdatedAt", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "Spec", EmitDefaultValue = false)]
        public Spec Spec { get; set; }

        [DataMember(Name = "JoinTokens", EmitDefaultValue = false)]
        public JoinTokens JoinTokens { get; set; }
    }
}
