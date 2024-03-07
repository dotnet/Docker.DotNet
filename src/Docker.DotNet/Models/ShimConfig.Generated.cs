using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ShimConfig // (types.ShimConfig)
    {
        [JsonPropertyName("Binary")]
        public string Binary { get; set; }

        [JsonPropertyName("Opts")]
        public object Opts { get; set; }
    }
}
