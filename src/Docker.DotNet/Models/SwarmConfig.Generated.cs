using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmConfig // (main.SwarmConfig)
    {
        public SwarmConfig()
        {
        }

        public SwarmConfig(Meta Meta)
        {
            if (Meta != null)
            {
                this.Version = Meta.Version;
                this.CreatedAt = Meta.CreatedAt;
                this.UpdatedAt = Meta.UpdatedAt;
            }
        }

        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("Version")]
        public Version Version { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("Spec")]
        public SwarmConfigSpec Spec { get; set; }
    }
}
