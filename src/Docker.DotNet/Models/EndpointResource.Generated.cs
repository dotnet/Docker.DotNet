using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class EndpointResource // (types.EndpointResource)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("EndpointID")]
        public string EndpointID { get; set; }

        [JsonPropertyName("MacAddress")]
        public string MacAddress { get; set; }

        [JsonPropertyName("IPv4Address")]
        public string IPv4Address { get; set; }

        [JsonPropertyName("IPv6Address")]
        public string IPv6Address { get; set; }
    }
}
