using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmUpdateConfigParameters // (main.SwarmUpdateConfigParameters)
    {
        [JsonPropertyName("Config")]
        public SwarmConfigSpec Config { get; set; }

        [QueryStringParameter("version", true)]
        public long Version { get; set; }
    }
}
