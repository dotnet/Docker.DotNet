using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerNode // (types.ContainerNode)
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("IP")]
        public string IPAddress { get; set; }

        [JsonPropertyName("Addr")]
        public string Addr { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cpus")]
        public long Cpus { get; set; }

        [JsonPropertyName("Memory")]
        public long Memory { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }
    }
}
