using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ExternalCA // (swarm.ExternalCA)
    {
        [JsonPropertyName("Protocol")]
        public string Protocol { get; set; }

        [JsonPropertyName("URL")]
        public string URL { get; set; }

        [JsonPropertyName("Options")]
        public IDictionary<string, string> Options { get; set; }

        [JsonPropertyName("CACert")]
        public string CACert { get; set; }
    }
}
