using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class IPAM // (network.IPAM)
    {
        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "Options")]
        public IDictionary<string, string> Options { get; set; }

        [DataMember(Name = "Config")]
        public IList<IPAMConfig> Config { get; set; }
    }
}
