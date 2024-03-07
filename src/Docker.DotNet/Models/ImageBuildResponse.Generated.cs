using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImageBuildResponse // (types.ImageBuildResponse)
    {
        [JsonPropertyName("Body")]
        public object Body { get; set; }

        [JsonPropertyName("OSType")]
        public string OSType { get; set; }
    }
}
