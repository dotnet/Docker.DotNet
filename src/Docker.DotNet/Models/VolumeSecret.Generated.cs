using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class VolumeSecret // (volume.Secret)
    {
        [JsonPropertyName("Key")]
        public string Key { get; set; }

        [JsonPropertyName("Secret")]
        public string Secret { get; set; }
    }
}
