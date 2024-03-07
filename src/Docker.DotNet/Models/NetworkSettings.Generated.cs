using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkSettings // (types.NetworkSettings)
    {
        public NetworkSettings()
        {
        }

        public NetworkSettings(NetworkSettingsBase NetworkSettingsBase, DefaultNetworkSettings DefaultNetworkSettings)
        {
            if (NetworkSettingsBase != null)
            {
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

            if (DefaultNetworkSettings != null)
            {
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

        [JsonPropertyName("Bridge")]
        public string Bridge { get; set; }

        [JsonPropertyName("SandboxID")]
        public string SandboxID { get; set; }

        [JsonPropertyName("HairpinMode")]
        public bool HairpinMode { get; set; }

        [JsonPropertyName("LinkLocalIPv6Address")]
        public string LinkLocalIPv6Address { get; set; }

        [JsonPropertyName("LinkLocalIPv6PrefixLen")]
        public long LinkLocalIPv6PrefixLen { get; set; }

        [JsonPropertyName("Ports")]
        public IDictionary<string, IList<PortBinding>> Ports { get; set; }

        [JsonPropertyName("SandboxKey")]
        public string SandboxKey { get; set; }

        [JsonPropertyName("SecondaryIPAddresses")]
        public IList<Address> SecondaryIPAddresses { get; set; }

        [JsonPropertyName("SecondaryIPv6Addresses")]
        public IList<Address> SecondaryIPv6Addresses { get; set; }

        [JsonPropertyName("EndpointID")]
        public string EndpointID { get; set; }

        [JsonPropertyName("Gateway")]
        public string Gateway { get; set; }

        [JsonPropertyName("GlobalIPv6Address")]
        public string GlobalIPv6Address { get; set; }

        [JsonPropertyName("GlobalIPv6PrefixLen")]
        public long GlobalIPv6PrefixLen { get; set; }

        [JsonPropertyName("IPAddress")]
        public string IPAddress { get; set; }

        [JsonPropertyName("IPPrefixLen")]
        public long IPPrefixLen { get; set; }

        [JsonPropertyName("IPv6Gateway")]
        public string IPv6Gateway { get; set; }

        [JsonPropertyName("MacAddress")]
        public string MacAddress { get; set; }

        [JsonPropertyName("Networks")]
        public IDictionary<string, EndpointSettings> Networks { get; set; }
    }
}
