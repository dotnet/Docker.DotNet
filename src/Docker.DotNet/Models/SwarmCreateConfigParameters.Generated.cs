using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmCreateConfigParameters // (main.SwarmCreateConfigParameters)
    {
        [JsonPropertyName("Config")]
        public SwarmConfigSpec Config { get; set; }
    }
}
