using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
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
                this.EnableIPv6 = NetworkCreate.EnableIPv6;
                this.IPAM = NetworkCreate.IPAM;
                this.Internal = NetworkCreate.Internal;
                this.Options = NetworkCreate.Options;
                this.Labels = NetworkCreate.Labels;
            }
        }

        [DataMember(Name = "CheckDuplicate", EmitDefaultValue = false)]
        public bool CheckDuplicate { get; set; }

        [DataMember(Name = "Driver", EmitDefaultValue = false)]
        public string Driver { get; set; }

        [DataMember(Name = "EnableIPv6", EmitDefaultValue = false)]
        public bool EnableIPv6 { get; set; }

        [DataMember(Name = "IPAM", EmitDefaultValue = false)]
        public IPAM IPAM { get; set; }

        [DataMember(Name = "Internal", EmitDefaultValue = false)]
        public bool Internal { get; set; }

        [DataMember(Name = "Options", EmitDefaultValue = false)]
        public IDictionary<string, string> Options { get; set; }

        [DataMember(Name = "Labels", EmitDefaultValue = false)]
        public IDictionary<string, string> Labels { get; set; }

        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public string Name { get; set; }
    }
}
