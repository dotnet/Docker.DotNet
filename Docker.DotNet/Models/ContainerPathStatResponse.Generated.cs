using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerPathStatResponse // (types.ContainerPathStat)
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "size")]
        public long Size { get; set; }

        [DataMember(Name = "mode")]
        public uint Mode { get; set; }

        [DataMember(Name = "mtime")]
        public System.DateTime Mtime { get; set; }

        [DataMember(Name = "linkTarget")]
        public string LinkTarget { get; set; }
    }
}
