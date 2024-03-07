using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkTask // (network.Task)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("EndpointID")]
        public string EndpointID { get; set; }

        [JsonPropertyName("EndpointIP")]
        public string EndpointIP { get; set; }

        [JsonPropertyName("Info")]
        public IDictionary<string, string> Info { get; set; }
    }
}
