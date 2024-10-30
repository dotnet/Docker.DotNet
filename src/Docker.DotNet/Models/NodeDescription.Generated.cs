using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NodeDescription // (swarm.NodeDescription)
    {
        [JsonPropertyName("Hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("Platform")]
        public Platform Platform { get; set; }

        [JsonPropertyName("Resources")]
        public SwarmResources Resources { get; set; }

        [JsonPropertyName("Engine")]
        public EngineDescription Engine { get; set; }

        [JsonPropertyName("TLSInfo")]
        public TLSInfo TLSInfo { get; set; }

        [JsonPropertyName("CSIInfo")]
        public IList<NodeCSIInfo> CSIInfo { get; set; }
    }
}
