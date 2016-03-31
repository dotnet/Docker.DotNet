using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CPUStats // (types.CPUStats)
    {
        [DataMember(Name = "cpu_usage")]
        public CPUUsage CPUUsage { get; set; }

        [DataMember(Name = "system_cpu_usage")]
        public ulong SystemUsage { get; set; }

        [DataMember(Name = "throttling_data")]
        public ThrottlingData ThrottlingData { get; set; }
    }
}
