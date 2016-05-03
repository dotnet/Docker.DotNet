using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerWaitResponse // (types.ContainerWaitResponse)
    {
        [DataMember(Name = "StatusCode")]
        public long StatusCode { get; set; }
    }
}
