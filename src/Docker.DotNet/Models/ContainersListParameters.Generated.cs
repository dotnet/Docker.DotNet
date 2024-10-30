using System.Collections.Generic;

namespace Docker.DotNet.Models
{
    public class ContainersListParameters // (main.ContainersListParameters)
    {
        [QueryStringParameter("size", false, typeof(BoolQueryStringConverter))]
        public bool? Size { get; set; }

        [QueryStringParameter("all", false, typeof(BoolQueryStringConverter))]
        public bool? All { get; set; }

        [QueryStringParameter("since", false)]
        public string Since { get; set; }

        [QueryStringParameter("before", false)]
        public string Before { get; set; }

        [QueryStringParameter("limit", false)]
        public long? Limit { get; set; }

        [QueryStringParameter("filters", false, typeof(MapQueryStringConverter))]
        public IDictionary<string, IDictionary<string, bool>> Filters { get; set; }
    }
}
