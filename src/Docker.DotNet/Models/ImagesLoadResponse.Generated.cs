using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImagesLoadResponse // (types.ImageLoadResponse)
    {
        [JsonPropertyName("Body")]
        public object Body { get; set; }

        [JsonPropertyName("JSON")]
        public bool JSON { get; set; }
    }
}
