using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworksCreateResponse // (types.NetworkCreateResponse)
    {
        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("Warning")]
        public string Warning { get; set; }
    }
}
