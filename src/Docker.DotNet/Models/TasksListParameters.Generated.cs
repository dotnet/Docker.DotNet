using System.Collections.Generic;

namespace Docker.DotNet.Models
{
    public class TasksListParameters // (main.TasksListParameters)
    {
        [QueryStringParameter("filters", false, typeof(MapQueryStringConverter))]
        public IDictionary<string, IDictionary<string, bool>> Filters { get; set; }
    }
}
