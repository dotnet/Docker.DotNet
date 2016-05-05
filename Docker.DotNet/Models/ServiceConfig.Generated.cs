using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ServiceConfig // (registry.ServiceConfig)
    {
        [DataMember(Name = "InsecureRegistryCIDRs", EmitDefaultValue = false)]
        public IList<NetIPNet> InsecureRegistryCIDRs { get; set; }

        [DataMember(Name = "IndexConfigs", EmitDefaultValue = false)]
        public IDictionary<string, IndexInfo> IndexConfigs { get; set; }

        [DataMember(Name = "Mirrors", EmitDefaultValue = false)]
        public IList<string> Mirrors { get; set; }
    }
}
