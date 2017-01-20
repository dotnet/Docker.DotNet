using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SwarmInitParameters // (swarm.InitRequest)
    {
        [DataMember(Name = "ListenAddr", EmitDefaultValue = false)]
        public string ListenAddr { get; set; }

        [DataMember(Name = "AdvertiseAddr", EmitDefaultValue = false)]
        public string AdvertiseAddr { get; set; }

        [DataMember(Name = "ForceNewCluster", EmitDefaultValue = false)]
        public bool ForceNewCluster { get; set; }

        [DataMember(Name = "Spec", EmitDefaultValue = false)]
        public Spec Spec { get; set; }

        [DataMember(Name = "AutoLockManagers", EmitDefaultValue = false)]
        public bool AutoLockManagers { get; set; }
    }
}
