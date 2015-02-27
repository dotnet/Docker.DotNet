using System;

namespace Docker.DotNet.Models
{
    public class StopContainerParameters
    {
        [QueryStringParameter("t", false, typeof (TimeSpanSecondsQueryStringConverter))]
        public TimeSpan? Wait { get; set; }

        public StopContainerParameters()
        {
        }
    }
}