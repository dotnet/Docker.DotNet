using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Plugin // (types.Plugin)
    {
        [JsonPropertyName("Config")]
        public PluginConfig Config { get; set; }

        [JsonPropertyName("Enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("PluginReference")]
        public string PluginReference { get; set; }

        [JsonPropertyName("Settings")]
        public PluginSettings Settings { get; set; }
    }
}
