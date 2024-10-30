
namespace Docker.DotNet.Models
{
    public class ImageLoadParameters // (main.ImageLoadParameters)
    {
        [QueryStringParameter("quiet", true, typeof(BoolQueryStringConverter))]
        public bool Quiet { get; set; }
    }
}
