using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginInstallParameters // (main.PluginInstallParameters)
    {
        [QueryStringParameter("remote", true)]
        public string Remote { get; set; }

        [QueryStringParameter("name", false)]
        public string Name { get; set; }

        [JsonPropertyName("RegistryAuth")]
        public AuthConfig RegistryAuth { get; set; }

        [JsonPropertyName("Privileges")]
        public IList<PluginPrivilege> Privileges { get; set; }
    }
}
