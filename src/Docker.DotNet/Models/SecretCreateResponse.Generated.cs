using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SecretCreateResponse // (main.SecretCreateResponse)
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
    }
}
