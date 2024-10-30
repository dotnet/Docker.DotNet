using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class IndexInfo // (registry.IndexInfo)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Mirrors")]
        public IList<string> Mirrors { get; set; }

        [JsonPropertyName("Secure")]
        public bool Secure { get; set; }

        [JsonPropertyName("Official")]
        public bool Official { get; set; }
    }
}
