using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImageSearchResponse // (registry.SearchResult)
    {
        [JsonPropertyName("star_count")]
        public long StarCount { get; set; }

        [JsonPropertyName("is_official")]
        public bool IsOfficial { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("is_automated")]
        public bool IsAutomated { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
