using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageDeleteResponse // (types.ImageDelete)
    {
        [DataMember(Name = "Untagged")]
        public string Untagged { get; set; }

        [DataMember(Name = "Deleted")]
        public string Deleted { get; set; }
    }
}
