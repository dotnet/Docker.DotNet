using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class PlacementPreference // (swarm.PlacementPreference)
    {
        [JsonPropertyName("Spread")]
        public SpreadOver Spread { get; set; }
    }
}
