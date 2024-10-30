using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImageDeleteResponse // (types.ImageDeleteResponseItem)
    {
        [JsonPropertyName("Deleted")]
        public string Deleted { get; set; }

        [JsonPropertyName("Untagged")]
        public string Untagged { get; set; }
    }
}
