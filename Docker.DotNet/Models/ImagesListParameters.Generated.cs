using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesListParameters // (types.ImageListOptions)
    {
        [QueryStringParameter("filter", false)]
        public string MatchName { get; set; }

        [QueryStringParameter("all", false, typeof(BoolQueryStringConverter))]
        public bool? All { get; set; }

        [QueryStringParameter("filters", false)]
        public Args Filters { get; set; }
    }
}
