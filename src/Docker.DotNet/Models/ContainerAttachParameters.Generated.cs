
namespace Docker.DotNet.Models
{
    public class ContainerAttachParameters // (main.ContainerAttachParameters)
    {
        [QueryStringParameter("stream", false, typeof(BoolQueryStringConverter))]
        public bool? Stream { get; set; }

        [QueryStringParameter("stdin", false, typeof(BoolQueryStringConverter))]
        public bool? Stdin { get; set; }

        [QueryStringParameter("stdout", false, typeof(BoolQueryStringConverter))]
        public bool? Stdout { get; set; }

        [QueryStringParameter("stderr", false, typeof(BoolQueryStringConverter))]
        public bool? Stderr { get; set; }

        [QueryStringParameter("detachKeys", false)]
        public string DetachKeys { get; set; }

        [QueryStringParameter("logs", false)]
        public string Logs { get; set; }
    }
}
