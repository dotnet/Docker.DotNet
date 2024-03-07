using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkSettingsBase // (types.NetworkSettingsBase)
    {
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
    }
}
