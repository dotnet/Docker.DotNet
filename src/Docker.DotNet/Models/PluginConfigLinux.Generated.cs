using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfigLinux // (types.PluginConfigLinux)
    {
        [JsonPropertyName("AllowAllDevices")]
        public bool AllowAllDevices { get; set; }

        [JsonPropertyName("Capabilities")]
        public IList<string> Capabilities { get; set; }

        [JsonPropertyName("Devices")]
        public IList<PluginDevice> Devices { get; set; }
    }
}
