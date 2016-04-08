using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ServiceConfig // (registry.ServiceConfig)
    {
        [DataMember(Name = "InsecureRegistryCIDRs")]
        public IList<NetIPNet> InsecureRegistryCIDRs { get; set; }

        [DataMember(Name = "IndexConfigs")]
        public IDictionary<string, IndexInfo> IndexConfigs { get; set; }

        [DataMember(Name = "Mirrors")]
        public IList<string> Mirrors { get; set; }
    }
}
