using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Address // (network.Address)
    {
        [DataMember(Name = "Addr")]
        public string Addr { get; set; }

        [DataMember(Name = "PrefixLen")]
        public int PrefixLen { get; set; }
    }
}
