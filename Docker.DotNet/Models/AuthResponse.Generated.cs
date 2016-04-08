using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class AuthResponse // (types.AuthResponse)
    {
        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "IdentityToken")]
        public string IdentityToken { get; set; }
    }
}
