using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Endpoint // (swarm.Endpoint)
    {
        [JsonPropertyName("Spec")]
        public EndpointSpec Spec { get; set; }

        [JsonPropertyName("Ports")]
        public IList<PortConfig> Ports { get; set; }

        [JsonPropertyName("VirtualIPs")]
        public IList<EndpointVirtualIP> VirtualIPs { get; set; }
    }
}
