using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginMount // (types.PluginMount)
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Destination")]
        public string Destination { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Options")]
        public IList<string> Options { get; set; }

        [JsonPropertyName("Settable")]
        public IList<string> Settable { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}
