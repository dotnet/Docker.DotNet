using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesSearchParameters // (types.ImageSearchOptions)
    {
        [QueryStringParameter("term", false)]
        public string Term { get; set; }

        [DataMember(Name = "RegistryAuth")]
        public string RegistryAuth { get; set; }
    }
}
