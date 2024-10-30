using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class EndpointSpec // (swarm.EndpointSpec)
    {
        [JsonPropertyName("Mode")]
        public string Mode { get; set; }

        [JsonPropertyName("Ports")]
        public IList<PortConfig> Ports { get; set; }
    }
}
