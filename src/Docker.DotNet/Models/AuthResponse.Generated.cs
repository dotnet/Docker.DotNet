using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class AuthResponse // (types.AuthResponse)
    {
        [DataMember(Name = "Status", EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(Name = "IdentityToken", EmitDefaultValue = false)]
        public string IdentityToken { get; set; }
    }
}
