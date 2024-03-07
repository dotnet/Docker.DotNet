using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SecretReferenceFileTarget // (swarm.SecretReferenceFileTarget)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("UID")]
        public string UID { get; set; }

        [JsonPropertyName("GID")]
        public string GID { get; set; }

        [JsonPropertyName("Mode")]
        public uint Mode { get; set; }
    }
}
