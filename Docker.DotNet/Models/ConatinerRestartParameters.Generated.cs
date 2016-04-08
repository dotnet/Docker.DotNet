using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ConatinerRestartParameters // (main.ContainerRestartParameters)
    {
        [DataMember(Name = "WaitBeforeKillSeconds")]
        public uint WaitBeforeKillSeconds { get; set; }
    }
}
