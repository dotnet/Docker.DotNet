using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VolumeAttachment // (swarm.VolumeAttachment)
    {
        [DataMember(Name = "ID", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "Source", EmitDefaultValue = false)]
        public string Source { get; set; }

        [DataMember(Name = "Target", EmitDefaultValue = false)]
        public string Target { get; set; }
    }
}
