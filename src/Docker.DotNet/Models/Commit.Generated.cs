using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Commit // (types.Commit)
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("Expected")]
        public string Expected { get; set; }
    }
}
