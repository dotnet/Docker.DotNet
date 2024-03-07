using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class UsageData // (volume.UsageData)
    {
        [JsonPropertyName("RefCount")]
        public long RefCount { get; set; }

        [JsonPropertyName("Size")]
        public long Size { get; set; }
    }
}
