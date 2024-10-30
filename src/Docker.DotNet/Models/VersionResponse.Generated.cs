using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VersionResponse // (types.Version)
    {
        [JsonPropertyName("Components")]
        public IList<ComponentVersion> Components { get; set; }

        [JsonPropertyName("Version")]
        public string Version { get; set; }

        [JsonPropertyName("ApiVersion")]
        public string APIVersion { get; set; }

        [JsonPropertyName("MinAPIVersion")]
        public string MinAPIVersion { get; set; }

        [JsonPropertyName("GitCommit")]
        public string GitCommit { get; set; }

        [JsonPropertyName("GoVersion")]
        public string GoVersion { get; set; }

        [JsonPropertyName("Os")]
        public string Os { get; set; }

        [JsonPropertyName("Arch")]
        public string Arch { get; set; }

        [JsonPropertyName("KernelVersion")]
        public string KernelVersion { get; set; }

        [JsonPropertyName("Experimental")]
        public bool Experimental { get; set; }

        [JsonPropertyName("BuildTime")]
        public string BuildTime { get; set; }
    }
}
