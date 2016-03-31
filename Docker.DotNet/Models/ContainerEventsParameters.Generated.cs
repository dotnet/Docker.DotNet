using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerEventsParameters // (types.EventsOptions)
    {
        [QueryStringParameter("since", false)]
        public string Since { get; set; }

        [QueryStringParameter("until", false)]
        public string Until { get; set; }

        [QueryStringParameter("filters", false)]
        public Args Filters { get; set; }
    }
}
