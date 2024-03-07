using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmConfigSpec // (swarm.ConfigSpec)
    {
        public SwarmConfigSpec()
        {
        }

        public SwarmConfigSpec(Annotations Annotations)
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

        [JsonPropertyName("Templating")]
        public SwarmDriver Templating { get; set; }
    }
}
