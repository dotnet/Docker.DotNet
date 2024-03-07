using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ServiceConfig // (registry.ServiceConfig)
    {
        [JsonPropertyName("AllowNondistributableArtifactsCIDRs")]
        public IList<string> AllowNondistributableArtifactsCIDRs { get; set; }

        [JsonPropertyName("AllowNondistributableArtifactsHostnames")]
        public IList<string> AllowNondistributableArtifactsHostnames { get; set; }

        [JsonPropertyName("InsecureRegistryCIDRs")]
        public IList<string> InsecureRegistryCIDRs { get; set; }

        [JsonPropertyName("IndexConfigs")]
        public IDictionary<string, IndexInfo> IndexConfigs { get; set; }

        [JsonPropertyName("Mirrors")]
        public IList<string> Mirrors { get; set; }
    }
}
