using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfigureParameters // (main.PluginConfigureParameters)
    {
        [JsonPropertyName("Args")]
        public IList<string> Args { get; set; }
    }
}
