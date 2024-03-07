using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfig // (types.PluginConfig)
    {
        [JsonPropertyName("Args")]
        public PluginConfigArgs Args { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("DockerVersion")]
        public string DockerVersion { get; set; }

        [JsonPropertyName("Documentation")]
        public string Documentation { get; set; }

        [JsonPropertyName("Entrypoint")]
        public IList<string> Entrypoint { get; set; }

        [JsonPropertyName("Env")]
        public IList<PluginEnv> Env { get; set; }

        [JsonPropertyName("Interface")]
        public PluginConfigInterface Interface { get; set; }

        [JsonPropertyName("IpcHost")]
        public bool IpcHost { get; set; }

        [JsonPropertyName("Linux")]
        public PluginConfigLinux Linux { get; set; }

        [JsonPropertyName("Mounts")]
        public IList<PluginMount> Mounts { get; set; }

        [JsonPropertyName("Network")]
        public PluginConfigNetwork Network { get; set; }

        [JsonPropertyName("PidHost")]
        public bool PidHost { get; set; }

        [JsonPropertyName("PropagatedMount")]
        public string PropagatedMount { get; set; }

        [JsonPropertyName("User")]
        public PluginConfigUser User { get; set; }

        [JsonPropertyName("WorkDir")]
        public string WorkDir { get; set; }

        [JsonPropertyName("rootfs")]
        public PluginConfigRootfs Rootfs { get; set; }
    }
}
