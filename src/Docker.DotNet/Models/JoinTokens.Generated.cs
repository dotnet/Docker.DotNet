using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class JoinTokens // (swarm.JoinTokens)
    {
        [JsonPropertyName("Worker")]
        public string Worker { get; set; }

        [JsonPropertyName("Manager")]
        public string Manager { get; set; }
    }
}
