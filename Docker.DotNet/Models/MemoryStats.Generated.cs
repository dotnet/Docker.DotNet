using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class MemoryStats // (types.MemoryStats)
    {
        [DataMember(Name = "usage")]
        public ulong Usage { get; set; }

        [DataMember(Name = "max_usage")]
        public ulong MaxUsage { get; set; }

        [DataMember(Name = "stats")]
        public IDictionary<string, ulong> Stats { get; set; }

        [DataMember(Name = "failcnt")]
        public ulong Failcnt { get; set; }

        [DataMember(Name = "limit")]
        public ulong Limit { get; set; }
    }
}
