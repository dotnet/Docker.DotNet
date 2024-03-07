using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmService // (swarm.Service)
    {
        public SwarmService()
        {
        }

        public SwarmService(Meta Meta)
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
        public ServiceSpec Spec { get; set; }

        [JsonPropertyName("PreviousSpec")]
        public ServiceSpec PreviousSpec { get; set; }

        [JsonPropertyName("Endpoint")]
        public Endpoint Endpoint { get; set; }

        [JsonPropertyName("UpdateStatus")]
        public UpdateStatus UpdateStatus { get; set; }

        [JsonPropertyName("ServiceStatus")]
        public ServiceStatus ServiceStatus { get; set; }

        [JsonPropertyName("JobStatus")]
        public JobStatus JobStatus { get; set; }
    }
}
