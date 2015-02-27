using System.Collections.Generic;

namespace Docker.DotNet.Models
{
    public class ListImagesParameters
    {
        [QueryStringParameter("all", false, typeof (BoolQueryStringConverter))]
        public bool? All { get; set; }

        [QueryStringParameter("filters", false, typeof (JsonQueryStringConverter))]
        public IDictionary<string, IList<string>> Filters { get; set; }

        public ListImagesParameters()
        {
        }
    }
}