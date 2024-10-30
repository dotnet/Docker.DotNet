
namespace Docker.DotNet.Models
{
    public class ContainerPathStatParameters // (main.ContainerPathStatParameters)
    {
        [QueryStringParameter("path", true)]
        public string Path { get; set; }

        [QueryStringParameter("noOverwriteDirNonDir", false, typeof(BoolQueryStringConverter))]
        public bool? AllowOverwriteDirWithFile { get; set; }

        [QueryStringParameter("copyUIDGID", false, typeof(BoolQueryStringConverter))]
        public bool? CopyUidAndGid { get; set; }
    }
}
