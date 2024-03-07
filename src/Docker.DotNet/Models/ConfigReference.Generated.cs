using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ConfigReference // (network.ConfigReference)
    {
        [JsonPropertyName("Network")]
        public string Network { get; set; }
    }
}
