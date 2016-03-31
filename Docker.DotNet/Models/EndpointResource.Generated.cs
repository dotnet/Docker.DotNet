using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class EndpointResource // (types.EndpointResource)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "EndpointID")]
        public string EndpointID { get; set; }

        [DataMember(Name = "MacAddress")]
        public string MacAddress { get; set; }

        [DataMember(Name = "IPv4Address")]
        public string IPv4Address { get; set; }

        [DataMember(Name = "IPv6Address")]
        public string IPv6Address { get; set; }
    }
}
