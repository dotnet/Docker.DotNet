using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ClusterInfo // (swarm.ClusterInfo)
    {
        public ClusterInfo()
        {
        }

        public ClusterInfo(Meta Meta)
        {
            if (Meta != null)
            {
                this.Version = Meta.Version;
                this.CreatedAt = Meta.CreatedAt;
                this.UpdatedAt = Meta.UpdatedAt;
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
    }
}
