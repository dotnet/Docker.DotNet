using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginSpec // (runtime.PluginSpec)
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("remote")]
        public string Remote { get; set; }

        [JsonPropertyName("privileges")]
        public IList<RuntimePluginPrivilege> Privileges { get; set; }

        [JsonPropertyName("disabled")]
        public bool Disabled { get; set; }

        [JsonPropertyName("env")]
        public IList<string> Env { get; set; }
    }
}
