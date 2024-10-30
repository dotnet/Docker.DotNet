using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfigNetwork // (types.PluginConfigNetwork)
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}
