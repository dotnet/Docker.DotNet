using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CAConfig // (swarm.CAConfig)
    {
        [DataMember(Name = "NodeCertExpiry", EmitDefaultValue = false)]
        public long NodeCertExpiry { get; set; }

        [DataMember(Name = "ExternalCAs", EmitDefaultValue = false)]
        public IList<ExternalCA> ExternalCAs { get; set; }
    }
}
