namespace Docker.DotNet.Models
{
    public class ExtractArchiveToContainerParameters
    {
        [QueryStringParameter("path", true)]
        public string Path { get; set; }

        [QueryStringParameter("noOverwriteDirNonDir", false, typeof(BoolQueryStringConverter))]
        public bool? NoOverwriteDirNonDir { get; set; }
    }
}
