using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Spec // (swarm.Spec)
    {
        public Spec()
        {
        }

        public Spec(Annotations Annotations)
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

        [JsonPropertyName("Orchestration")]
        public OrchestrationConfig Orchestration { get; set; }

        [JsonPropertyName("Raft")]
        public RaftConfig Raft { get; set; }

        [JsonPropertyName("Dispatcher")]
        public DispatcherConfig Dispatcher { get; set; }

        [JsonPropertyName("CAConfig")]
        public CAConfig CAConfig { get; set; }

        [JsonPropertyName("TaskDefaults")]
        public TaskDefaults TaskDefaults { get; set; }

        [JsonPropertyName("EncryptionConfig")]
        public EncryptionConfig EncryptionConfig { get; set; }
    }
}
