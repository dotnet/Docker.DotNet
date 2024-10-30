using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Annotations // (swarm.Annotations)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }
    }
}
