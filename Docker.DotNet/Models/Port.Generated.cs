using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Port // (types.Port)
    {
        [DataMember(Name = "IP")]
        public string IP { get; set; }

        [DataMember(Name = "PrivatePort")]
        public int PrivatePort { get; set; }

        [DataMember(Name = "PublicPort")]
        public int PublicPort { get; set; }

        [DataMember(Name = "Type")]
        public string Type { get; set; }
    }
}
