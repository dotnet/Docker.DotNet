using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesCreateParameters // (types.ImageCreateOptions)
    {
        [QueryStringParameter("fromImage", false)]
        public string Parent { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [DataMember(Name = "RegistryAuth")]
        public string RegistryAuth { get; set; }
    }
}
