using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Platform // (swarm.Platform)
    {
        [JsonPropertyName("Architecture")]
        public string Architecture { get; set; }

        [JsonPropertyName("OS")]
        public string OS { get; set; }
    }
}
