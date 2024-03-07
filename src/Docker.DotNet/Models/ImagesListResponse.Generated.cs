using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImagesListResponse // (types.ImageSummary)
    {
        [JsonPropertyName("Containers")]
        public long Containers { get; set; }

        [JsonPropertyName("Created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("ParentId")]
        public string ParentID { get; set; }

        [JsonPropertyName("RepoDigests")]
        public IList<string> RepoDigests { get; set; }

        [JsonPropertyName("RepoTags")]
        public IList<string> RepoTags { get; set; }

        [JsonPropertyName("SharedSize")]
        public long SharedSize { get; set; }

        [JsonPropertyName("Size")]
        public long Size { get; set; }

        [JsonPropertyName("VirtualSize")]
        public long VirtualSize { get; set; }
    }
}
