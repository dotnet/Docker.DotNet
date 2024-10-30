using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Topology // (swarm.Topology)
    {
        [JsonPropertyName("Segments")]
        public IDictionary<string, string> Segments { get; set; }
    }
}
