using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginDevice // (types.PluginDevice)
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Path")]
        public string Path { get; set; }

        [JsonPropertyName("Settable")]
        public IList<string> Settable { get; set; }
    }
}
