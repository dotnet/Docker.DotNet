using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworksCreateParameters // (types.NetworkCreateRequest)
    {
        public NetworksCreateParameters()
        {
        }

        public NetworksCreateParameters(NetworkCreate NetworkCreate)
        {
            if (NetworkCreate != null)
            {
                this.CheckDuplicate = NetworkCreate.CheckDuplicate;
                this.Driver = NetworkCreate.Driver;
                this.Scope = NetworkCreate.Scope;
                this.EnableIPv6 = NetworkCreate.EnableIPv6;
                this.IPAM = NetworkCreate.IPAM;
                this.Internal = NetworkCreate.Internal;
                this.Attachable = NetworkCreate.Attachable;
                this.Ingress = NetworkCreate.Ingress;
                this.ConfigOnly = NetworkCreate.ConfigOnly;
                this.ConfigFrom = NetworkCreate.ConfigFrom;
                this.Options = NetworkCreate.Options;
                this.Labels = NetworkCreate.Labels;
            }
        }

        [JsonPropertyName("CheckDuplicate")]
        public bool CheckDuplicate { get; set; }

        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

        [JsonPropertyName("Scope")]
        public string Scope { get; set; }

        [JsonPropertyName("EnableIPv6")]
        public bool EnableIPv6 { get; set; }

        [JsonPropertyName("IPAM")]
        public IPAM IPAM { get; set; }

        [JsonPropertyName("Internal")]
        public bool Internal { get; set; }

        [JsonPropertyName("Attachable")]
        public bool Attachable { get; set; }

        [JsonPropertyName("Ingress")]
        public bool Ingress { get; set; }

        [JsonPropertyName("ConfigOnly")]
        public bool ConfigOnly { get; set; }

        [JsonPropertyName("ConfigFrom")]
        public ConfigReference ConfigFrom { get; set; }

        [JsonPropertyName("Options")]
        public IDictionary<string, string> Options { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }
}
