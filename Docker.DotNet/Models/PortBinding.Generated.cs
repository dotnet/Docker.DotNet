using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PortBinding // (nat.PortBinding)
    {
        [DataMember(Name = "HostIp")]
        public string HostIP { get; set; }

        [DataMember(Name = "HostPort")]
        public string HostPort { get; set; }
    }
}
