using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkConnectParameters // (types.NetworkConnect)
    {
        [JsonPropertyName("Container")]
        public string Container { get; set; }

        [JsonPropertyName("EndpointConfig")]
        public EndpointSettings EndpointConfig { get; set; }
    }
}
