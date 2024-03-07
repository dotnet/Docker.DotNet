using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class DiscreteGenericResource // (swarm.DiscreteGenericResource)
    {
        [JsonPropertyName("Kind")]
        public string Kind { get; set; }

        [JsonPropertyName("Value")]
        public long Value { get; set; }
    }
}
