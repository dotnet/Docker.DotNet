using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImagePushParameters // (main.ImagePushParameters)
    {
        [QueryStringParameter("fromImage", false)]
        public string ImageID { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [JsonPropertyName("RegistryAuth")]
        public AuthConfig RegistryAuth { get; set; }
    }
}
