using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class EndpointIPAMConfig // (network.EndpointIPAMConfig)
    {
        [DataMember(Name = "IPv4Address")]
        public string IPv4Address { get; set; }

        [DataMember(Name = "IPv6Address")]
        public string IPv6Address { get; set; }
    }
}
