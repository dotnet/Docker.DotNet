using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class RuntimePluginPrivilege // (runtime.PluginPrivilege)
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("value")]
        public IList<string> Value { get; set; }
    }
}
