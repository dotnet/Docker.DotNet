using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VolumeSecret // (volume.Secret)
    {
        [DataMember(Name = "Key", EmitDefaultValue = false)]
        public string Key { get; set; }

        [DataMember(Name = "Secret", EmitDefaultValue = false)]
        public string Secret { get; set; }
    }
}
