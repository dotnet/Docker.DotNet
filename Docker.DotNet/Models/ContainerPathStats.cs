using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerPathStats
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Size")]
        public int Size { get; set; }

        [DataMember(Name = "Mode")]
        public int Mode { get; set; }

        [DataMember(Name = "MTime")]
        public DateTime ModifiedTime { get; set; }

        [DataMember(Name = "LinkTarget")]
        public string LinkTarget { get; set; } 
    }
}
