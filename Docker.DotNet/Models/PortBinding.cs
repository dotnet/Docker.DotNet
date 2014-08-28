using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PortBinding
    {
        [DataMember(Name = "HostIp")]
        public string HostIp { get; set; }

        [DataMember(Name = "HostPort")]
        public string HostPort { get; set; }

        public PortBinding()
        {
        }
    }
}