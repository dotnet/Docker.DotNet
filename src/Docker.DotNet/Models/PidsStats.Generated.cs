using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PidsStats // (types.PidsStats)
    {
        [JsonPropertyName("current")]
        public ulong Current { get; set; }

        [JsonPropertyName("limit")]
        public ulong Limit { get; set; }
    }
}
