using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfigInterface // (types.PluginConfigInterface)
    {
        [JsonPropertyName("ProtocolScheme")]
        public string ProtocolScheme { get; set; }

        [JsonPropertyName("Socket")]
        public string Socket { get; set; }

        [JsonPropertyName("Types")]
        public IList<string> Types { get; set; }
    }
}
