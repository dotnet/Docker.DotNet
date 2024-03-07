using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginsInfo // (types.PluginsInfo)
    {
        [JsonPropertyName("Volume")]
        public IList<string> Volume { get; set; }

        [JsonPropertyName("Network")]
        public IList<string> Network { get; set; }

        [JsonPropertyName("Authorization")]
        public IList<string> Authorization { get; set; }

        [JsonPropertyName("Log")]
        public IList<string> Log { get; set; }
    }
}
