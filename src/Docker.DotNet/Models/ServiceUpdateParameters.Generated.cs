using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ServiceUpdateParameters // (main.ServiceUpdateParameters)
    {
        [DataMember(Name = "Service", EmitDefaultValue = false)]
        public ServiceSpec Service { get; set; }

        [QueryStringParameter("version", true)]
        public long Version { get; set; }

        [QueryStringParameter("registryauthfrom", false)]
        public string RegistryAuthFrom { get; set; }

        [QueryStringParameter("rollback", false)]
        public string Rollback { get; set; }

        [DataMember(Name = "RegistryAuth", EmitDefaultValue = false)]
        public AuthConfig RegistryAuth { get; set; }
    }
}
