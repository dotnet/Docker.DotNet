using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Ulimit // (units.Ulimit)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Hard")]
        public long Hard { get; set; }

        [JsonPropertyName("Soft")]
        public long Soft { get; set; }
    }
}
