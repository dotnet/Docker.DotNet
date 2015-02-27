using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Port
    {
        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "PrivatePort")]
        public int PrivatePort { get; set; }

        [DataMember(Name = "PublicPort")]
        public int PublicPort { get; set; }

        public Port()
        {
        }
    }
}