using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerPathStatParameters // (types.CopyToContainerOptions)
    {
        [DataMember(Name = "ContainerID")]
        public string ContainerID { get; set; }

        [QueryStringParameter("path", false)]
        public string Path { get; set; }

        [DataMember(Name = "Content")]
        public object Content { get; set; }

        [QueryStringParameter("noOverwriteDirNonDir", false, typeof(BoolQueryStringConverter))]
        public bool? AllowOverwriteDirWithFile { get; set; }
    }
}
