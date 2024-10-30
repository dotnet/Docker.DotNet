using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmInspectResponse // (swarm.Swarm)
    {
        public SwarmInspectResponse()
        {
        }

        public SwarmInspectResponse(ClusterInfo ClusterInfo)
        {
            if (ClusterInfo != null)
            {
                this.ID = ClusterInfo.ID;
                this.Version = ClusterInfo.Version;
                this.CreatedAt = ClusterInfo.CreatedAt;
                this.UpdatedAt = ClusterInfo.UpdatedAt;
                this.Spec = ClusterInfo.Spec;
                this.TLSInfo = ClusterInfo.TLSInfo;
                this.RootRotationInProgress = ClusterInfo.RootRotationInProgress;
                this.DefaultAddrPool = ClusterInfo.DefaultAddrPool;
                this.SubnetSize = ClusterInfo.SubnetSize;
                this.DataPathPort = ClusterInfo.DataPathPort;
            }
        }

        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("Version")]
        public Version Version { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("Spec")]
        public Spec Spec { get; set; }

        [JsonPropertyName("TLSInfo")]
        public TLSInfo TLSInfo { get; set; }

        [JsonPropertyName("RootRotationInProgress")]
        public bool RootRotationInProgress { get; set; }

        [JsonPropertyName("DefaultAddrPool")]
        public IList<string> DefaultAddrPool { get; set; }

        [JsonPropertyName("SubnetSize")]
        public uint SubnetSize { get; set; }

        [JsonPropertyName("DataPathPort")]
        public uint DataPathPort { get; set; }

        [JsonPropertyName("JoinTokens")]
        public JoinTokens JoinTokens { get; set; }
    }
}
