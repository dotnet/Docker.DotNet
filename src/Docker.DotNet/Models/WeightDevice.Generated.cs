using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class WeightDevice // (blkiodev.WeightDevice)
    {
        [JsonPropertyName("Path")]
        public string Path { get; set; }

        [JsonPropertyName("Weight")]
        public ushort Weight { get; set; }
    }
}
