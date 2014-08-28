namespace Docker.DotNet.Models
{
    public class MonitorDockerEventsParameters
    {
        [QueryStringParameter("since", false)]
        public long? Since { get; set; }

        [QueryStringParameter("until", false)]
        public long? Until { get; set; }

        public MonitorDockerEventsParameters()
        {
        }
    }
}