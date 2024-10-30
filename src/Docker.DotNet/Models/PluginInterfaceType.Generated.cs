using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginInterfaceType // (types.PluginInterfaceType)
    {
        [JsonPropertyName("Capability")]
        public string Capability { get; set; }

        [JsonPropertyName("Prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("Version")]
        public string Version { get; set; }
    }
}
