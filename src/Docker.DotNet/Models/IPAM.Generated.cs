using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class IPAM // (network.IPAM)
    {
        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

        [JsonPropertyName("Options")]
        public IDictionary<string, string> Options { get; set; }

        [JsonPropertyName("Config")]
        public IList<IPAMConfig> Config { get; set; }
    }
}
