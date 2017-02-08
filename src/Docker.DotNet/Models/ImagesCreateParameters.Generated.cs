using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesCreateParameters // (main.ImageCreateParameters)
    {
        [QueryStringParameter("fromImage", true)]
        public string Parent { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [DataMember(Name = "RegistryAuth", EmitDefaultValue = false)]
        public string RegistryAuth { get; set; }
    }
}
