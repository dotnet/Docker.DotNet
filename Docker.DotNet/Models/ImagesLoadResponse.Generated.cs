using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesLoadResponse // (types.ImageLoadResponse)
    {
        [DataMember(Name = "Body")]
        public object Body { get; set; }

        [DataMember(Name = "JSON")]
        public bool JSON { get; set; }
    }
}
