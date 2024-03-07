using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NodeListResponse // (swarm.Node)
    {
        public NodeListResponse()
        {
        }

        public NodeListResponse(Meta Meta)
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
        public NodeUpdateParameters Spec { get; set; }

        [JsonPropertyName("Description")]
        public NodeDescription Description { get; set; }

        [JsonPropertyName("Status")]
        public NodeStatus Status { get; set; }

        [JsonPropertyName("ManagerStatus")]
        public ManagerStatus ManagerStatus { get; set; }
    }
}
