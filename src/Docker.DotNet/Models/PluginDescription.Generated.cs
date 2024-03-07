using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginDescription // (swarm.PluginDescription)
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
