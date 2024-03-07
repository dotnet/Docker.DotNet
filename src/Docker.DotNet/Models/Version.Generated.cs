using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Version // (swarm.Version)
    {
        [JsonPropertyName("Index")]
        public ulong Index { get; set; }
    }
}
