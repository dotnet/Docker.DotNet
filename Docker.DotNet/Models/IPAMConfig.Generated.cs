using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class IPAMConfig // (network.IPAMConfig)
    {
        [DataMember(Name = "Subnet")]
        public string Subnet { get; set; }

        [DataMember(Name = "IPRange")]
        public string IPRange { get; set; }

        [DataMember(Name = "Gateway")]
        public string Gateway { get; set; }

        [DataMember(Name = "AuxiliaryAddresses")]
        public IDictionary<string, string> AuxAddress { get; set; }
    }
}
