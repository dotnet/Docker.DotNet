using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageInspectParameters // (main.ImageInspectParameters)
    {
        [DataMember(Name = "IncludeSize")]
        public bool IncludeSize { get; set; }
    }
}
