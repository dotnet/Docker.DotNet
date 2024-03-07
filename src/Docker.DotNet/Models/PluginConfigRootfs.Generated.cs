using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfigRootfs // (types.PluginConfigRootfs)
    {
        [JsonPropertyName("diff_ids")]
        public IList<string> DiffIds { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
