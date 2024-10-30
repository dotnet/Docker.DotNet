using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NodeCSIInfo // (swarm.NodeCSIInfo)
    {
        [JsonPropertyName("PluginName")]
        public string PluginName { get; set; }

        [JsonPropertyName("NodeID")]
        public string NodeID { get; set; }

        [JsonPropertyName("MaxVolumesPerNode")]
        public long MaxVolumesPerNode { get; set; }

        [JsonPropertyName("AccessibleTopology")]
        public Topology AccessibleTopology { get; set; }
    }
}
