using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ServiceStatus // (swarm.ServiceStatus)
    {
        [DataMember(Name = "RunningTasks", EmitDefaultValue = false)]
        public ulong RunningTasks { get; set; }

        [DataMember(Name = "DesiredTasks", EmitDefaultValue = false)]
        public ulong DesiredTasks { get; set; }
    }
}
