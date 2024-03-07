using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SpreadOver // (swarm.SpreadOver)
    {
        [JsonPropertyName("SpreadDescriptor")]
        public string SpreadDescriptor { get; set; }
    }
}
