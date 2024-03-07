using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmUnlockParameters // (main.SwarmUnlockParameters)
    {
        [JsonPropertyName("UnlockKey")]
        public string UnlockKey { get; set; }
    }
}
