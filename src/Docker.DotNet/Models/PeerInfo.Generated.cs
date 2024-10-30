using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PeerInfo // (network.PeerInfo)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("IP")]
        public string IP { get; set; }
    }
}
