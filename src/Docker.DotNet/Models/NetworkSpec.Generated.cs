using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkSpec // (swarm.NetworkSpec)
    {
        public NetworkSpec()
        {
        }

        public NetworkSpec(Annotations Annotations)
        {
            if (Annotations != null)
            {
                this.Name = Annotations.Name;
                this.Labels = Annotations.Labels;
            }
        }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("DriverConfiguration")]
        public SwarmDriver DriverConfiguration { get; set; }

        [JsonPropertyName("IPv6Enabled")]
        public bool IPv6Enabled { get; set; }

        [JsonPropertyName("Internal")]
        public bool Internal { get; set; }

        [JsonPropertyName("Attachable")]
        public bool Attachable { get; set; }

        [JsonPropertyName("Ingress")]
        public bool Ingress { get; set; }

        [JsonPropertyName("IPAMOptions")]
        public IPAMOptions IPAMOptions { get; set; }

        [JsonPropertyName("ConfigFrom")]
        public ConfigReference ConfigFrom { get; set; }

        [JsonPropertyName("Scope")]
        public string Scope { get; set; }
    }
}
