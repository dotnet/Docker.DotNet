using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagePushParameters // (types.ImagePushOptions)
    {
        [QueryStringParameter("fromImage", false)]
        public string ImageID { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [DataMember(Name = "RegistryAuth")]
        public string RegistryAuth { get; set; }
    }
}
