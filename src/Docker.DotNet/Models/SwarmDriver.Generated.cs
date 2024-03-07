using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmDriver // (swarm.Driver)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Options")]
        public IDictionary<string, string> Options { get; set; }
    }
}
