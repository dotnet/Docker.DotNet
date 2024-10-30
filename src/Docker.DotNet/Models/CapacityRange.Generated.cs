using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class CapacityRange // (volume.CapacityRange)
    {
        [JsonPropertyName("RequiredBytes")]
        public long RequiredBytes { get; set; }

        [JsonPropertyName("LimitBytes")]
        public long LimitBytes { get; set; }
    }
}
