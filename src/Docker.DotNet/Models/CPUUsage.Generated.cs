using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class CPUUsage // (types.CPUUsage)
    {
        [JsonPropertyName("total_usage")]
        public ulong TotalUsage { get; set; }

        [JsonPropertyName("percpu_usage")]
        public IList<ulong> PercpuUsage { get; set; }

        [JsonPropertyName("usage_in_kernelmode")]
        public ulong UsageInKernelmode { get; set; }

        [JsonPropertyName("usage_in_usermode")]
        public ulong UsageInUsermode { get; set; }
    }
}
