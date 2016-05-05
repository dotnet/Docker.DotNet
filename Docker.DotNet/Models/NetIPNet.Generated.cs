using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetIPNet // (registry.NetIPNet)
    {
        [DataMember(Name = "IP", EmitDefaultValue = false)]
        public IList<byte> IP { get; set; }

        [DataMember(Name = "Mask", EmitDefaultValue = false)]
        public IList<byte> Mask { get; set; }
    }
}
