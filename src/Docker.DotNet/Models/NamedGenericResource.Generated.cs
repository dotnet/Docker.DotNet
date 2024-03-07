using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NamedGenericResource // (swarm.NamedGenericResource)
    {
        [JsonPropertyName("Kind")]
        public string Kind { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }
    }
}
