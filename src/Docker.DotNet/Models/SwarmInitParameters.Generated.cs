using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmInitParameters // (swarm.InitRequest)
    {
        [JsonPropertyName("ListenAddr")]
        public string ListenAddr { get; set; }

        [JsonPropertyName("AdvertiseAddr")]
        public string AdvertiseAddr { get; set; }

        [JsonPropertyName("DataPathAddr")]
        public string DataPathAddr { get; set; }

        [JsonPropertyName("DataPathPort")]
        public uint DataPathPort { get; set; }

        [JsonPropertyName("ForceNewCluster")]
        public bool ForceNewCluster { get; set; }

        [JsonPropertyName("Spec")]
        public Spec Spec { get; set; }

        [JsonPropertyName("AutoLockManagers")]
        public bool AutoLockManagers { get; set; }

        [JsonPropertyName("Availability")]
        public string Availability { get; set; }

        [JsonPropertyName("DefaultAddrPool")]
        public IList<string> DefaultAddrPool { get; set; }

        [JsonPropertyName("SubnetSize")]
        public uint SubnetSize { get; set; }
    }
}
