using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class StorageStats // (types.StorageStats)
    {
        [JsonPropertyName("read_count_normalized")]
        public ulong ReadCountNormalized { get; set; }

        [JsonPropertyName("read_size_bytes")]
        public ulong ReadSizeBytes { get; set; }

        [JsonPropertyName("write_count_normalized")]
        public ulong WriteCountNormalized { get; set; }

        [JsonPropertyName("write_size_bytes")]
        public ulong WriteSizeBytes { get; set; }
    }
}
