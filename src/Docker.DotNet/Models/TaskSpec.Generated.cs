using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class TaskSpec // (swarm.TaskSpec)
    {
        [JsonPropertyName("ContainerSpec")]
        public ContainerSpec ContainerSpec { get; set; }

        [JsonPropertyName("PluginSpec")]
        public PluginSpec PluginSpec { get; set; }

        [JsonPropertyName("NetworkAttachmentSpec")]
        public NetworkAttachmentSpec NetworkAttachmentSpec { get; set; }

        [JsonPropertyName("Resources")]
        public ResourceRequirements Resources { get; set; }

        [JsonPropertyName("RestartPolicy")]
        public SwarmRestartPolicy RestartPolicy { get; set; }

        [JsonPropertyName("Placement")]
        public Placement Placement { get; set; }

        [JsonPropertyName("Networks")]
        public IList<NetworkAttachmentConfig> Networks { get; set; }

        [JsonPropertyName("LogDriver")]
        public SwarmDriver LogDriver { get; set; }

        [JsonPropertyName("ForceUpdate")]
        public ulong ForceUpdate { get; set; }

        [JsonPropertyName("Runtime")]
        public string Runtime { get; set; }
    }
}
