using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class IPAMOptions // (swarm.IPAMOptions)
    {
        [JsonPropertyName("Driver")]
        public SwarmDriver Driver { get; set; }

        [JsonPropertyName("Configs")]
        public IList<SwarmIPAMConfig> Configs { get; set; }
    }
}
