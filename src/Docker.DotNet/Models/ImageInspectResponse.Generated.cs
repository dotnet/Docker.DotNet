using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImageInspectResponse // (types.ImageInspect)
    {
        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("RepoTags")]
        public IList<string> RepoTags { get; set; }

        [JsonPropertyName("RepoDigests")]
        public IList<string> RepoDigests { get; set; }

        [JsonPropertyName("Parent")]
        public string Parent { get; set; }

        [JsonPropertyName("Comment")]
        public string Comment { get; set; }

        [JsonPropertyName("Created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("Container")]
        public string Container { get; set; }

        [JsonPropertyName("ContainerConfig")]
        public Config ContainerConfig { get; set; }

        [JsonPropertyName("DockerVersion")]
        public string DockerVersion { get; set; }

        [JsonPropertyName("Author")]
        public string Author { get; set; }

        [JsonPropertyName("Config")]
        public Config Config { get; set; }

        [JsonPropertyName("Architecture")]
        public string Architecture { get; set; }

        [JsonPropertyName("Variant")]
        public string Variant { get; set; }

        [JsonPropertyName("Os")]
        public string Os { get; set; }

        [JsonPropertyName("OsVersion")]
        public string OsVersion { get; set; }

        [JsonPropertyName("Size")]
        public long Size { get; set; }

        [JsonPropertyName("VirtualSize")]
        public long VirtualSize { get; set; }

        [JsonPropertyName("GraphDriver")]
        public GraphDriverData GraphDriver { get; set; }

        [JsonPropertyName("RootFS")]
        public RootFS RootFS { get; set; }

        [JsonPropertyName("Metadata")]
        public ImageMetadata Metadata { get; set; }
    }
}
