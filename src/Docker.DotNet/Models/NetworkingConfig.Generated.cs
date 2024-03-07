using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkingConfig // (network.NetworkingConfig)
    {
        [JsonPropertyName("EndpointsConfig")]
        public IDictionary<string, EndpointSettings> EndpointsConfig { get; set; }
    }
}
