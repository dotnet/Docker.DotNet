using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageDelete // (types.ImageDelete)
    {
        [DataMember(Name = "Untagged", EmitDefaultValue = false)]
        public string Untagged { get; set; }

        [DataMember(Name = "Deleted", EmitDefaultValue = false)]
        public string Deleted { get; set; }
    }
}
