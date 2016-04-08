using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CPUUsage // (types.CPUUsage)
    {
        [DataMember(Name = "total_usage")]
        public ulong TotalUsage { get; set; }

        [DataMember(Name = "percpu_usage")]
        public IList<ulong> PercpuUsage { get; set; }

        [DataMember(Name = "usage_in_kernelmode")]
        public ulong UsageInKernelmode { get; set; }

        [DataMember(Name = "usage_in_usermode")]
        public ulong UsageInUsermode { get; set; }
    }
}
