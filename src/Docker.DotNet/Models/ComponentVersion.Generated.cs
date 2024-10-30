using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ComponentVersion // (types.ComponentVersion)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Version")]
        public string Version { get; set; }

        [JsonPropertyName("Details")]
        public IDictionary<string, string> Details { get; set; }
    }
}
