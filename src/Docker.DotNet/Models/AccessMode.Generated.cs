using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class AccessMode // (volume.AccessMode)
    {
        [DataMember(Name = "Scope", EmitDefaultValue = false)]
        public string Scope { get; set; }

        [DataMember(Name = "Sharing", EmitDefaultValue = false)]
        public string Sharing { get; set; }

        [DataMember(Name = "MountVolume", EmitDefaultValue = false)]
        public TypeMount MountVolume { get; set; }

        [DataMember(Name = "BlockVolume", EmitDefaultValue = false)]
        public TypeBlock BlockVolume { get; set; }
    }
}
