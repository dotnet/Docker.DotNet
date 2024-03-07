using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SELinuxContext // (swarm.SELinuxContext)
    {
        [JsonPropertyName("Disable")]
        public bool Disable { get; set; }

        [JsonPropertyName("User")]
        public string User { get; set; }

        [JsonPropertyName("Role")]
        public string Role { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Level")]
        public string Level { get; set; }
    }
}
