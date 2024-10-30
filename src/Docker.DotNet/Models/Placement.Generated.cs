using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Placement // (swarm.Placement)
    {
        [JsonPropertyName("Constraints")]
        public IList<string> Constraints { get; set; }

        [JsonPropertyName("Preferences")]
        public IList<PlacementPreference> Preferences { get; set; }

        [JsonPropertyName("MaxReplicas")]
        public ulong MaxReplicas { get; set; }

        [JsonPropertyName("Platforms")]
        public IList<Platform> Platforms { get; set; }
    }
}
