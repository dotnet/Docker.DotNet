using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Peer // (swarm.Peer)
    {
        [JsonPropertyName("NodeID")]
        public string NodeID { get; set; }

        [JsonPropertyName("Addr")]
        public string Addr { get; set; }
    }
}
