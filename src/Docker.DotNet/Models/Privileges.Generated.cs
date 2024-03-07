using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Privileges // (swarm.Privileges)
    {
        [JsonPropertyName("CredentialSpec")]
        public CredentialSpec CredentialSpec { get; set; }

        [JsonPropertyName("SELinuxContext")]
        public SELinuxContext SELinuxContext { get; set; }
    }
}
