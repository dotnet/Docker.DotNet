using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagePushParameters // (main.ImagePushParameters)
    {
        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }
    }
}
