using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class EndpointVirtualIP // (swarm.EndpointVirtualIP)
    {
        [JsonPropertyName("NetworkID")]
        public string NetworkID { get; set; }

        [JsonPropertyName("Addr")]
        public string Addr { get; set; }
    }
}
