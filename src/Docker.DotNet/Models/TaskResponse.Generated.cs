using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class TaskResponse // (swarm.Task)
    {
        public TaskResponse()
        {
        }

        public TaskResponse(Meta Meta, Annotations Annotations)
        {
            if (Meta != null)
            {
                this.Version = Meta.Version;
                this.CreatedAt = Meta.CreatedAt;
                this.UpdatedAt = Meta.UpdatedAt;
            }

            if (Annotations != null)
            {
                this.Name = Annotations.Name;
                this.Labels = Annotations.Labels;
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

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("Spec")]
        public TaskSpec Spec { get; set; }

        [JsonPropertyName("ServiceID")]
        public string ServiceID { get; set; }

        [JsonPropertyName("Slot")]
        public long Slot { get; set; }

        [JsonPropertyName("NodeID")]
        public string NodeID { get; set; }

        [JsonPropertyName("Status")]
        public TaskStatus Status { get; set; }

        [JsonPropertyName("DesiredState")]
        public TaskState DesiredState { get; set; }

        [JsonPropertyName("NetworksAttachments")]
        public IList<NetworkAttachment> NetworksAttachments { get; set; }

        [JsonPropertyName("GenericResources")]
        public IList<GenericResource> GenericResources { get; set; }

        [JsonPropertyName("JobIteration")]
        public Version JobIteration { get; set; }

        [JsonPropertyName("Volumes")]
        public IList<VolumeAttachment> Volumes { get; set; }
    }
}
