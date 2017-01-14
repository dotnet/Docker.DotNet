using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesPullParameters // (main.ImagePullParameters)
    {
        [QueryStringParameter("all", false, typeof(BoolQueryStringConverter))]
        public bool? All { get; set; }

        [QueryStringParameter("fromImage", true)]
        public string Parent { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [DataMember(Name = "RegistryAuth", EmitDefaultValue = false)]
        public string RegistryAuth { get; set; }
    }
}
