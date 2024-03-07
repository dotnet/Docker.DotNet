using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class EngineDescription // (swarm.EngineDescription)
    {
        [JsonPropertyName("EngineVersion")]
        public string EngineVersion { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("Plugins")]
        public IList<PluginDescription> Plugins { get; set; }
    }
}
