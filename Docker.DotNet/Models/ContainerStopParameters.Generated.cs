using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerStopParameters // (main.ContainerStopParameters)
    {
        [DataMember(Name = "WaitBeforeKillSeconds")]
        public uint WaitBeforeKillSeconds { get; set; }
    }
}
