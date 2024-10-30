using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class AuthConfig // (registry.AuthConfig)
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("auth")]
        public string Auth { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("serveraddress")]
        public string ServerAddress { get; set; }

        [JsonPropertyName("identitytoken")]
        public string IdentityToken { get; set; }

        [JsonPropertyName("registrytoken")]
        public string RegistryToken { get; set; }
    }
}
