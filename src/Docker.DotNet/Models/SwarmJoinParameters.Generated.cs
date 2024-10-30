using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmJoinParameters // (swarm.JoinRequest)
    {
        [JsonPropertyName("ListenAddr")]
        public string ListenAddr { get; set; }

        [JsonPropertyName("AdvertiseAddr")]
        public string AdvertiseAddr { get; set; }

        [JsonPropertyName("DataPathAddr")]
        public string DataPathAddr { get; set; }

        [JsonPropertyName("RemoteAddrs")]
        public IList<string> RemoteAddrs { get; set; }

        [JsonPropertyName("JoinToken")]
        public string JoinToken { get; set; }

        [JsonPropertyName("Availability")]
        public string Availability { get; set; }
    }
}
