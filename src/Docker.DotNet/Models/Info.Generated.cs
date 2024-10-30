using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Info // (swarm.Info)
    {
        [JsonPropertyName("NodeID")]
        public string NodeID { get; set; }

        [JsonPropertyName("NodeAddr")]
        public string NodeAddr { get; set; }

        [JsonPropertyName("LocalNodeState")]
        public string LocalNodeState { get; set; }

        [JsonPropertyName("ControlAvailable")]
        public bool ControlAvailable { get; set; }

        [JsonPropertyName("Error")]
        public string Error { get; set; }

        [JsonPropertyName("RemoteManagers")]
        public IList<Peer> RemoteManagers { get; set; }

        [JsonPropertyName("Nodes")]
        public long Nodes { get; set; }

        [JsonPropertyName("Managers")]
        public long Managers { get; set; }

        [JsonPropertyName("Cluster")]
        public ClusterInfo Cluster { get; set; }

        [JsonPropertyName("Warnings")]
        public IList<string> Warnings { get; set; }
    }
}
