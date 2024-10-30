using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmIPAMConfig // (swarm.IPAMConfig)
    {
        [JsonPropertyName("Subnet")]
        public string Subnet { get; set; }

        [JsonPropertyName("Range")]
        public string Range { get; set; }

        [JsonPropertyName("Gateway")]
        public string Gateway { get; set; }
    }
}
