using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkDisconnectParameters // (types.NetworkDisconnect)
    {
        [JsonPropertyName("Container")]
        public string Container { get; set; }

        [JsonPropertyName("Force")]
        public bool Force { get; set; }
    }
}
