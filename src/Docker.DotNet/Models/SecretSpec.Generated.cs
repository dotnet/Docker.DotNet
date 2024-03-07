using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SecretSpec // (swarm.SecretSpec)
    {
        public SecretSpec()
        {
        }

        public SecretSpec(Annotations Annotations)
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

        [JsonPropertyName("Data")]
        public IList<byte> Data { get; set; }

        [JsonPropertyName("Driver")]
        public SwarmDriver Driver { get; set; }

        [JsonPropertyName("Templating")]
        public SwarmDriver Templating { get; set; }
    }
}
