using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkAddressPool // (types.NetworkAddressPool)
    {
        [JsonPropertyName("Base")]
        public string Base { get; set; }

        [JsonPropertyName("Size")]
        public long Size { get; set; }
    }
}
