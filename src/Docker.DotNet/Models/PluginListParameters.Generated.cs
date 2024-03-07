using System.Collections.Generic;

namespace Docker.DotNet.Models
{
    public class PluginListParameters // (main.PluginListParameters)
    {
        [QueryStringParameter("filters", false, typeof(MapQueryStringConverter))]
        public IDictionary<string, IDictionary<string, bool>> Filters { get; set; }
    }
}
