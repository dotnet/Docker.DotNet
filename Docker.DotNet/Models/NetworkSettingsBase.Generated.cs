using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkSettingsBase // (types.NetworkSettingsBase)
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
        public long LinkLocalIPv6PrefixLen { get; set; }

        [DataMember(Name = "Ports")]
        public IDictionary<string, IList<PortBinding>> Ports { get; set; }

        [DataMember(Name = "SandboxKey")]
        public string SandboxKey { get; set; }

        [DataMember(Name = "SecondaryIPAddresses")]
        public IList<Address> SecondaryIPAddresses { get; set; }

        [DataMember(Name = "SecondaryIPv6Addresses")]
        public IList<Address> SecondaryIPv6Addresses { get; set; }
    }
}
