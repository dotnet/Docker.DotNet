using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkListResponse // (types.NetworkResource)
    {
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "Scope", EmitDefaultValue = false)]
        public string Scope { get; set; }

        [DataMember(Name = "Driver", EmitDefaultValue = false)]
        public string Driver { get; set; }

        [DataMember(Name = "EnableIPv6", EmitDefaultValue = false)]
        public bool EnableIPv6 { get; set; }

        [DataMember(Name = "IPAM", EmitDefaultValue = false)]
        public IPAM IPAM { get; set; }

        [DataMember(Name = "Internal", EmitDefaultValue = false)]
        public bool Internal { get; set; }

        [DataMember(Name = "Containers", EmitDefaultValue = false)]
        public IDictionary<string, EndpointResource> Containers { get; set; }

        [DataMember(Name = "Options", EmitDefaultValue = false)]
        public IDictionary<string, string> Options { get; set; }

        [DataMember(Name = "Labels", EmitDefaultValue = false)]
        public IDictionary<string, string> Labels { get; set; }
    }
}
