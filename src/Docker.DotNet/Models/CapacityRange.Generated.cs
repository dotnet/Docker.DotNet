using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CapacityRange // (volume.CapacityRange)
    {
        [DataMember(Name = "RequiredBytes", EmitDefaultValue = false)]
        public long RequiredBytes { get; set; }

        [DataMember(Name = "LimitBytes", EmitDefaultValue = false)]
        public long LimitBytes { get; set; }
    }
}
