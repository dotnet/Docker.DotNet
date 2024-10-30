using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginSettings // (types.PluginSettings)
    {
        [JsonPropertyName("Args")]
        public IList<string> Args { get; set; }

        [JsonPropertyName("Devices")]
        public IList<PluginDevice> Devices { get; set; }

        [JsonPropertyName("Env")]
        public IList<string> Env { get; set; }

        [JsonPropertyName("Mounts")]
        public IList<PluginMount> Mounts { get; set; }
    }
}
