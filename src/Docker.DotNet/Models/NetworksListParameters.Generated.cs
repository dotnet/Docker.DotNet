using System.Collections.Generic;

namespace Docker.DotNet.Models
{
    public class NetworksListParameters // (main.NetworksListParameters)
    {
        [QueryStringParameter("filters", false, typeof(MapQueryStringConverter))]
        public IDictionary<string, IDictionary<string, bool>> Filters { get; set; }
    }
}
