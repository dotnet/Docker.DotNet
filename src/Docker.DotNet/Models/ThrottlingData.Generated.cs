using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ThrottlingData // (types.ThrottlingData)
    {
        [JsonPropertyName("periods")]
        public ulong Periods { get; set; }

        [JsonPropertyName("throttled_periods")]
        public ulong ThrottledPeriods { get; set; }

        [JsonPropertyName("throttled_time")]
        public ulong ThrottledTime { get; set; }
    }
}
