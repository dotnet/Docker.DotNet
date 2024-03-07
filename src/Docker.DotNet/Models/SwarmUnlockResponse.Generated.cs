using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmUnlockResponse // (main.SwarmUnlockResponse)
    {
        [JsonPropertyName("UnlockKey")]
        public string UnlockKey { get; set; }
    }
}
