using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkResponse // (types.NetworkResource)
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("Created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("Scope")]
        public string Scope { get; set; }

        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

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

        [JsonPropertyName("ConfigFrom")]
        public ConfigReference ConfigFrom { get; set; }

        [JsonPropertyName("ConfigOnly")]
        public bool ConfigOnly { get; set; }

        [JsonPropertyName("Containers")]
        public IDictionary<string, EndpointResource> Containers { get; set; }

        [JsonPropertyName("Options")]
        public IDictionary<string, string> Options { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("Peers")]
        public IList<PeerInfo> Peers { get; set; }

        [JsonPropertyName("Services")]
        public IDictionary<string, ServiceInfo> Services { get; set; }
    }
}
