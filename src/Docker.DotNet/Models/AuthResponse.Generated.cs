using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class AuthResponse // (registry.AuthenticateOKBody)
    {
        [JsonPropertyName("IdentityToken")]
        public string IdentityToken { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }
    }
}
