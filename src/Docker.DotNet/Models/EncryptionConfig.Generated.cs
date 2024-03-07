using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class EncryptionConfig // (swarm.EncryptionConfig)
    {
        [JsonPropertyName("AutoLockManagers")]
        public bool AutoLockManagers { get; set; }
    }
}
