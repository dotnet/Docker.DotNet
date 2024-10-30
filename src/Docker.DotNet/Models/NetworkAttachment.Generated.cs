using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkAttachment // (swarm.NetworkAttachment)
    {
        [JsonPropertyName("Network")]
        public Network Network { get; set; }

        [JsonPropertyName("Addresses")]
        public IList<string> Addresses { get; set; }
    }
}
