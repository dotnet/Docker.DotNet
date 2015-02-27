namespace Docker.DotNet.Models
{
    public class GetContainerLogsParameters
    {
        [QueryStringParameter("follow", false, typeof (BoolQueryStringConverter))]
        public bool? Follow { get; set; }

        [QueryStringParameter("stdout", false, typeof (BoolQueryStringConverter))]
        public bool? Stdout { get; set; }

        [QueryStringParameter("stderr", false, typeof (BoolQueryStringConverter))]
        public bool? Stderr { get; set; }

        [QueryStringParameter("timestamps", false, typeof (BoolQueryStringConverter))]
        public bool? Timestamps { get; set; }

        [QueryStringParameter("tail", false, typeof (ContainerLogsTailModeQueryStringConverter))]
        public IContainerLogsTailMode Tail { get; set; }

        public GetContainerLogsParameters()
        {
        }
    }
}