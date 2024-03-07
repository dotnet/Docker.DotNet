using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmConfigReference // (swarm.ConfigReference)
    {
        [JsonPropertyName("File")]
        public ConfigReferenceFileTarget File { get; set; }

        [JsonPropertyName("Runtime")]
        public ConfigReferenceRuntimeTarget Runtime { get; set; }

        [JsonPropertyName("ConfigID")]
        public string ConfigID { get; set; }

        [JsonPropertyName("ConfigName")]
        public string ConfigName { get; set; }
    }
}
