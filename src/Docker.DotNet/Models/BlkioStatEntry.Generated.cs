using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class BlkioStatEntry // (types.BlkioStatEntry)
    {
        [JsonPropertyName("major")]
        public ulong Major { get; set; }

        [JsonPropertyName("minor")]
        public ulong Minor { get; set; }

        [JsonPropertyName("op")]
        public string Op { get; set; }

        [JsonPropertyName("value")]
        public ulong Value { get; set; }
    }
}
