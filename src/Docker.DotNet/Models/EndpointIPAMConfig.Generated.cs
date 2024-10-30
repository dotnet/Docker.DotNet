using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class EndpointIPAMConfig // (network.EndpointIPAMConfig)
    {
        [JsonPropertyName("IPv4Address")]
        public string IPv4Address { get; set; }

        [JsonPropertyName("IPv6Address")]
        public string IPv6Address { get; set; }

        [JsonPropertyName("LinkLocalIPs")]
        public IList<string> LinkLocalIPs { get; set; }
    }
}
