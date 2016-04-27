using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetIPNet // (registry.NetIPNet)
    {
        [DataMember(Name = "IP")]
        public IList<byte> IP { get; set; }

        [DataMember(Name = "Mask")]
        public IList<byte> Mask { get; set; }
    }
}
