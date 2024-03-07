using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class GenericResource // (swarm.GenericResource)
    {
        [JsonPropertyName("NamedResourceSpec")]
        public NamedGenericResource NamedResourceSpec { get; set; }

        [JsonPropertyName("DiscreteResourceSpec")]
        public DiscreteGenericResource DiscreteResourceSpec { get; set; }
    }
}
