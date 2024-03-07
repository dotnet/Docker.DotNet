using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PluginConfigUser // (types.PluginConfigUser)
    {
        [JsonPropertyName("GID")]
        public uint GID { get; set; }

        [JsonPropertyName("UID")]
        public uint UID { get; set; }
    }
}
