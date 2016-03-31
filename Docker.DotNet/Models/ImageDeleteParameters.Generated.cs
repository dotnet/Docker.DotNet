using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageDeleteParameters // (types.ImageRemoveOptions)
    {
        [DataMember(Name = "ImageID")]
        public string ImageID { get; set; }

        [QueryStringParameter("force", false, typeof(BoolQueryStringConverter))]
        public bool? Force { get; set; }

        [QueryStringParameter("noprune", false, typeof(BoolQueryStringConverter))]
        public bool? PruneChildren { get; set; }
    }
}
