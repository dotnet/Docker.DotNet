using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Address // (network.Address)
    {
        [JsonPropertyName("Addr")]
        public string Addr { get; set; }

        [JsonPropertyName("PrefixLen")]
        public long PrefixLen { get; set; }
    }
}
