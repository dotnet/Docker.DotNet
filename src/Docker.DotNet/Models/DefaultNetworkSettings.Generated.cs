using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class DefaultNetworkSettings // (types.DefaultNetworkSettings)
    {
        [JsonPropertyName("EndpointID")]
        public string EndpointID { get; set; }

        [JsonPropertyName("Gateway")]
        public string Gateway { get; set; }

        [JsonPropertyName("GlobalIPv6Address")]
        public string GlobalIPv6Address { get; set; }

        [JsonPropertyName("GlobalIPv6PrefixLen")]
        public long GlobalIPv6PrefixLen { get; set; }

        [JsonPropertyName("IPAddress")]
        public string IPAddress { get; set; }

        [JsonPropertyName("IPPrefixLen")]
        public long IPPrefixLen { get; set; }

        [JsonPropertyName("IPv6Gateway")]
        public string IPv6Gateway { get; set; }

        [JsonPropertyName("MacAddress")]
        public string MacAddress { get; set; }
    }
}
