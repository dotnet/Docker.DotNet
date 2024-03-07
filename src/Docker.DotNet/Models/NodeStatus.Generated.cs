using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NodeStatus // (swarm.NodeStatus)
    {
        [JsonPropertyName("State")]
        public string State { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }

        [JsonPropertyName("Addr")]
        public string Addr { get; set; }
    }
}
