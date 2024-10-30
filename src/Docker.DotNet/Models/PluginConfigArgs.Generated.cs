using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfigArgs // (types.PluginConfigArgs)
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Settable")]
        public IList<string> Settable { get; set; }

        [JsonPropertyName("Value")]
        public IList<string> Value { get; set; }
    }
}
