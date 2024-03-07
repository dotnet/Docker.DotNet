using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Network // (swarm.Network)
    {
        public Network()
        {
        }

        public Network(Meta Meta)
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
        public NetworkSpec Spec { get; set; }

        [JsonPropertyName("DriverState")]
        public SwarmDriver DriverState { get; set; }

        [JsonPropertyName("IPAMOptions")]
        public IPAMOptions IPAMOptions { get; set; }
    }
}
