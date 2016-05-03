using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class DefaultNetworkSettings // (types.DefaultNetworkSettings)
    {
        [DataMember(Name = "EndpointID")]
        public string EndpointID { get; set; }

        [DataMember(Name = "Gateway")]
        public string Gateway { get; set; }

        [DataMember(Name = "GlobalIPv6Address")]
        public string GlobalIPv6Address { get; set; }

        [DataMember(Name = "GlobalIPv6PrefixLen")]
        public long GlobalIPv6PrefixLen { get; set; }

        [DataMember(Name = "IPAddress")]
        public string IPAddress { get; set; }

        [DataMember(Name = "IPPrefixLen")]
        public long IPPrefixLen { get; set; }

        [DataMember(Name = "IPv6Gateway")]
        public string IPv6Gateway { get; set; }

        [DataMember(Name = "MacAddress")]
        public string MacAddress { get; set; }
    }
}
