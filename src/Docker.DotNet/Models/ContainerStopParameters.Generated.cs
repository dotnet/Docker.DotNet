using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerStopParameters // (main.ContainerStopParameters)
    {
        [QueryStringParameter("t", false)]
        public uint? WaitBeforeKillSeconds { get; set; }

        [QueryStringParameter("signal", false)]
        public string Signal { get; set; }
    }
}
