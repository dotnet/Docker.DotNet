using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class AuthConfigParameters // (main.AuthConfigParameters)
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "auth")]
        public string Auth { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "serveraddress")]
        public string ServerAddress { get; set; }

        [DataMember(Name = "identitytoken")]
        public string IdentityToken { get; set; }

        [DataMember(Name = "registrytoken")]
        public string RegistryToken { get; set; }
    }
}
