using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmCreateConfigResponse // (main.SwarmCreateConfigResponse)
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
    }
}
