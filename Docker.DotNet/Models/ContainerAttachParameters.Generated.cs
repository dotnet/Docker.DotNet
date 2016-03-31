using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerAttachParameters // (types.ContainerAttachOptions)
    {
        [DataMember(Name = "ContainerID")]
        public string ContainerID { get; set; }

        [QueryStringParameter("stream", false, typeof(BoolQueryStringConverter))]
        public bool? Stream { get; set; }

        [QueryStringParameter("stdin", false, typeof(BoolQueryStringConverter))]
        public bool? Stdin { get; set; }

        [QueryStringParameter("stdout", false, typeof(BoolQueryStringConverter))]
        public bool? Stdout { get; set; }

        [QueryStringParameter("stderr", false, typeof(BoolQueryStringConverter))]
        public bool? Stderr { get; set; }

        [QueryStringParameter("detachKeys", false)]
        public string DetachKeys { get; set; }
    }
}
