using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageLoadParameters // (main.ImageLoadParameters)
    {
        [QueryStringParameter("quiet", false, typeof(BoolQueryStringConverter))]
        public bool? Quiet { get; set; }
    }
}
