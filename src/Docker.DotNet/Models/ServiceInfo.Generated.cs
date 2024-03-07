using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ServiceInfo // (network.ServiceInfo)
    {
        [JsonPropertyName("VIP")]
        public string VIP { get; set; }

        [JsonPropertyName("Ports")]
        public IList<string> Ports { get; set; }

        [JsonPropertyName("LocalLBIndex")]
        public long LocalLBIndex { get; set; }

        [JsonPropertyName("Tasks")]
        public IList<NetworkTask> Tasks { get; set; }
    }
}
