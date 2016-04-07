using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class MountPoint // (types.MountPoint)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Source")]
        public string Source { get; set; }

        [DataMember(Name = "Destination")]
        public string Destination { get; set; }

        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "Mode")]
        public string Mode { get; set; }

        [DataMember(Name = "RW")]
        public bool RW { get; set; }

        [DataMember(Name = "Propagation")]
        public string Propagation { get; set; }
    }
}
