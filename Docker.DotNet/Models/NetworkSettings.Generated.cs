using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkSettings // (types.NetworkSettings)
    {
        public NetworkSettings()
        {
        }

        public NetworkSettings(NetworkSettingsBase NetworkSettingsBase, DefaultNetworkSettings DefaultNetworkSettings)
        {
            if (NetworkSettingsBase != null)            {
                this.Bridge = NetworkSettingsBase.Bridge;
                this.SandboxID = NetworkSettingsBase.SandboxID;
                this.HairpinMode = NetworkSettingsBase.HairpinMode;
                this.LinkLocalIPv6Address = NetworkSettingsBase.LinkLocalIPv6Address;
                this.LinkLocalIPv6PrefixLen = NetworkSettingsBase.LinkLocalIPv6PrefixLen;
                this.Ports = NetworkSettingsBase.Ports;
                this.SandboxKey = NetworkSettingsBase.SandboxKey;
                this.SecondaryIPAddresses = NetworkSettingsBase.SecondaryIPAddresses;
                this.SecondaryIPv6Addresses = NetworkSettingsBase.SecondaryIPv6Addresses;
            }

            if (DefaultNetworkSettings != null)            {
                this.EndpointID = DefaultNetworkSettings.EndpointID;
                this.Gateway = DefaultNetworkSettings.Gateway;
                this.GlobalIPv6Address = DefaultNetworkSettings.GlobalIPv6Address;
                this.GlobalIPv6PrefixLen = DefaultNetworkSettings.GlobalIPv6PrefixLen;
                this.IPAddress = DefaultNetworkSettings.IPAddress;
                this.IPPrefixLen = DefaultNetworkSettings.IPPrefixLen;
                this.IPv6Gateway = DefaultNetworkSettings.IPv6Gateway;
                this.MacAddress = DefaultNetworkSettings.MacAddress;
            }
        }

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
