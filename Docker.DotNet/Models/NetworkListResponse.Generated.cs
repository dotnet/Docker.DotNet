using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkListResponse // (types.NetworkResource)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Id")]
        public string ID { get; set; }

        [DataMember(Name = "Scope")]
        public string Scope { get; set; }

        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "EnableIPv6")]
        public bool EnableIPv6 { get; set; }

        [DataMember(Name = "IPAM")]
        public IPAM IPAM { get; set; }

        [DataMember(Name = "Internal")]
        public bool Internal { get; set; }

        [DataMember(Name = "Containers")]
        public IDictionary<string, EndpointResource> Containers { get; set; }

        [DataMember(Name = "Options")]
        public IDictionary<string, string> Options { get; set; }

        [DataMember(Name = "Labels")]
        public IDictionary<string, string> Labels { get; set; }
    }
}
