using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerKillParameters // (main.ContainerKillParameters)
    {
        [DataMember(Name = "Signal")]
        public string Signal { get; set; }
    }
}
