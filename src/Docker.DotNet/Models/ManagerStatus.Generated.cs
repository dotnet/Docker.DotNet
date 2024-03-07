using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ManagerStatus // (swarm.ManagerStatus)
    {
        [JsonPropertyName("Leader")]
        public bool Leader { get; set; }

        [JsonPropertyName("Reachability")]
        public string Reachability { get; set; }

        [JsonPropertyName("Addr")]
        public string Addr { get; set; }
    }
}
