using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginUpgradeParameters // (main.PluginUpgradeParameters)
    {
        [QueryStringParameter("remote", true)]
        public string Remote { get; set; }

        [JsonPropertyName("RegistryAuth")]
        public AuthConfig RegistryAuth { get; set; }

        [JsonPropertyName("Privileges")]
        public IList<PluginPrivilege> Privileges { get; set; }
    }
}
