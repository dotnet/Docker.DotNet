using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class MemoryStats // (types.MemoryStats)
    {
        [JsonPropertyName("usage")]
        public ulong Usage { get; set; }

        [JsonPropertyName("max_usage")]
        public ulong MaxUsage { get; set; }

        [JsonPropertyName("stats")]
        public IDictionary<string, ulong> Stats { get; set; }

        [JsonPropertyName("failcnt")]
        public ulong Failcnt { get; set; }

        [JsonPropertyName("limit")]
        public ulong Limit { get; set; }

        [JsonPropertyName("commitbytes")]
        public ulong Commit { get; set; }

        [JsonPropertyName("commitpeakbytes")]
        public ulong CommitPeak { get; set; }

        [JsonPropertyName("privateworkingset")]
        public ulong PrivateWorkingSet { get; set; }
    }
}
