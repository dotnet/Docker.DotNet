using System;

namespace Docker.DotNet.Models
{
    public class RestartContainerParameters
    {
        [QueryStringParameter("t", false, typeof (TimeSpanSecondsQueryStringConverter))]
        public TimeSpan? Wait { get; set; }

        public RestartContainerParameters()
        {
        }
    }
}