using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class BlkioStatEntry // (types.BlkioStatEntry)
    {
        [DataMember(Name = "major")]
        public ulong Major { get; set; }

        [DataMember(Name = "minor")]
        public ulong Minor { get; set; }

        [DataMember(Name = "op")]
        public string Op { get; set; }

        [DataMember(Name = "value")]
        public ulong Value { get; set; }
    }
}
