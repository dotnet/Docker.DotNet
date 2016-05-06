using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesSearchParameters // (main.ImageSearchParameters)
    {
        [QueryStringParameter("term", false)]
        public string Term { get; set; }

        [DataMember(Name = "RegistryAuth", EmitDefaultValue = false)]
        public string RegistryAuth { get; set; }
    }
}
