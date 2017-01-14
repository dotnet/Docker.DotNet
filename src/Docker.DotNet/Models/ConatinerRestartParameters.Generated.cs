using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ConatinerRestartParameters // (main.ContainerRestartParameters)
    {
        [QueryStringParameter("t", false)]
        public uint? WaitBeforeKillSeconds { get; set; }
    }
}
