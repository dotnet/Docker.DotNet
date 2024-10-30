using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class CredentialSpec // (swarm.CredentialSpec)
    {
        [JsonPropertyName("Config")]
        public string Config { get; set; }

        [JsonPropertyName("File")]
        public string File { get; set; }

        [JsonPropertyName("Registry")]
        public string Registry { get; set; }
    }
}
