using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerStartParameters // (main.ContainerStartParameters)
    {
        [DataMember(Name = "DetachKeys")]
        public string DetachKeys { get; set; }
    }
}
