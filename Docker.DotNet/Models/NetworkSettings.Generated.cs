using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkSettings // (types.NetworkSettings)
    {
        [DataMember(Name = "Bridge")]
        public string Bridge { get; set; }

        [DataMember(Name = "SandboxID")]
        public string SandboxID { get; set; }

        [DataMember(Name = "HairpinMode")]
        public bool HairpinMode { get; set; }

        [DataMember(Name = "LinkLocalIPv6Address")]
        public string LinkLocalIPv6Address { get; set; }

        [DataMember(Name = "LinkLocalIPv6PrefixLen")]
        public int LinkLocalIPv6PrefixLen { get; set; }

        [DataMember(Name = "Ports")]
        public IDictionary<string, IList<PortBinding>> Ports { get; set; }

        [DataMember(Name = "SandboxKey")]
        public string SandboxKey { get; set; }

        [DataMember(Name = "SecondaryIPAddresses")]
        public IList<Address> SecondaryIPAddresses { get; set; }

        [DataMember(Name = "SecondaryIPv6Addresses")]
        public IList<Address> SecondaryIPv6Addresses { get; set; }

        [DataMember(Name = "EndpointID")]
        public string EndpointID { get; set; }

        [DataMember(Name = "Gateway")]
        public string Gateway { get; set; }

        [DataMember(Name = "GlobalIPv6Address")]
        public string GlobalIPv6Address { get; set; }

        [DataMember(Name = "GlobalIPv6PrefixLen")]
        public int GlobalIPv6PrefixLen { get; set; }

        [DataMember(Name = "IPAddress")]
        public string IPAddress { get; set; }

        [DataMember(Name = "IPPrefixLen")]
        public int IPPrefixLen { get; set; }

        [DataMember(Name = "IPv6Gateway")]
        public string IPv6Gateway { get; set; }

        [DataMember(Name = "MacAddress")]
        public string MacAddress { get; set; }

        [DataMember(Name = "Networks")]
        public IDictionary<string, EndpointSettings> Networks { get; set; }
    }
}
