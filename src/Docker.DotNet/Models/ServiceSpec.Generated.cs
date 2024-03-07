using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ServiceSpec // (swarm.ServiceSpec)
    {
        public ServiceSpec()
        {
        }

        public ServiceSpec(Annotations Annotations)
        {
            if (Annotations != null)
            {
                this.Name = Annotations.Name;
                this.Labels = Annotations.Labels;
            }
        }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("TaskTemplate")]
        public TaskSpec TaskTemplate { get; set; }

        [JsonPropertyName("Mode")]
        public ServiceMode Mode { get; set; }

        [JsonPropertyName("UpdateConfig")]
        public SwarmUpdateConfig UpdateConfig { get; set; }

        [JsonPropertyName("RollbackConfig")]
        public SwarmUpdateConfig RollbackConfig { get; set; }

        [JsonPropertyName("Networks")]
        public IList<NetworkAttachmentConfig> Networks { get; set; }

        [JsonPropertyName("EndpointSpec")]
        public EndpointSpec EndpointSpec { get; set; }
    }
}
