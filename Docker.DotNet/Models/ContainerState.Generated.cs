using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerState // (types.ContainerState)
    {
        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "Running")]
        public bool Running { get; set; }

        [DataMember(Name = "Paused")]
        public bool Paused { get; set; }

        [DataMember(Name = "Restarting")]
        public bool Restarting { get; set; }

        [DataMember(Name = "OOMKilled")]
        public bool OOMKilled { get; set; }

        [DataMember(Name = "Dead")]
        public bool Dead { get; set; }

        [DataMember(Name = "Pid")]
        public long Pid { get; set; }

        [DataMember(Name = "ExitCode")]
        public long ExitCode { get; set; }

        [DataMember(Name = "Error")]
        public string Error { get; set; }

        [DataMember(Name = "StartedAt")]
        public string StartedAt { get; set; }

        [DataMember(Name = "FinishedAt")]
        public string FinishedAt { get; set; }
    }
}
