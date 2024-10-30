using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class CPUStats // (types.CPUStats)
    {
        [JsonPropertyName("cpu_usage")]
        public CPUUsage CPUUsage { get; set; }

        [JsonPropertyName("system_cpu_usage")]
        public ulong SystemUsage { get; set; }

        [JsonPropertyName("online_cpus")]
        public uint OnlineCPUs { get; set; }

        [JsonPropertyName("throttling_data")]
        public ThrottlingData ThrottlingData { get; set; }
    }
}
