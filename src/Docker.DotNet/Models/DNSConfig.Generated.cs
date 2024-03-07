using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class DNSConfig // (swarm.DNSConfig)
    {
        [JsonPropertyName("Nameservers")]
        public IList<string> Nameservers { get; set; }

        [JsonPropertyName("Search")]
        public IList<string> Search { get; set; }

        [JsonPropertyName("Options")]
        public IList<string> Options { get; set; }
    }
}
