
namespace Docker.DotNet.Models
{
    public class ImageInspectParameters // (main.ImageInspectParameters)
    {
        [QueryStringParameter("size", false, typeof(BoolQueryStringConverter))]
        public bool? IncludeSize { get; set; }
    }
}
