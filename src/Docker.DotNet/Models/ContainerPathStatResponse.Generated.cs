using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerPathStatResponse // (types.ContainerPathStat)
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("mode")]
        public uint Mode { get; set; }

        [JsonPropertyName("mtime")]
        public DateTime Mtime { get; set; }

        [JsonPropertyName("linkTarget")]
        public string LinkTarget { get; set; }
    }
}
