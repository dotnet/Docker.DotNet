using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class EndpointSettings // (network.EndpointSettings)
    {
        [JsonPropertyName("IPAMConfig")]
        public EndpointIPAMConfig IPAMConfig { get; set; }

        [JsonPropertyName("Links")]
        public IList<string> Links { get; set; }

        [JsonPropertyName("Aliases")]
        public IList<string> Aliases { get; set; }

        [JsonPropertyName("NetworkID")]
        public string NetworkID { get; set; }

        [JsonPropertyName("EndpointID")]
        public string EndpointID { get; set; }

        [JsonPropertyName("Gateway")]
        public string Gateway { get; set; }

        [JsonPropertyName("IPAddress")]
        public string IPAddress { get; set; }

        [JsonPropertyName("IPPrefixLen")]
        public long IPPrefixLen { get; set; }

        [JsonPropertyName("IPv6Gateway")]
        public string IPv6Gateway { get; set; }

        [JsonPropertyName("GlobalIPv6Address")]
        public string GlobalIPv6Address { get; set; }

        [JsonPropertyName("GlobalIPv6PrefixLen")]
        public long GlobalIPv6PrefixLen { get; set; }

        [JsonPropertyName("MacAddress")]
        public string MacAddress { get; set; }

        [JsonPropertyName("DriverOpts")]
        public IDictionary<string, string> DriverOpts { get; set; }
    }
}
