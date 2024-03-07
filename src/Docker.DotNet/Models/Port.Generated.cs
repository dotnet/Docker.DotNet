using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Port // (types.Port)
    {
        [JsonPropertyName("IP")]
        public string IP { get; set; }

        [JsonPropertyName("PrivatePort")]
        public ushort PrivatePort { get; set; }

        [JsonPropertyName("PublicPort")]
        public ushort PublicPort { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}
