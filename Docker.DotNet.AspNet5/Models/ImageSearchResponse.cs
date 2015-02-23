using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageSearchResponse
    {
        [DataMember(Name = "star_count")]
        public long StarCount { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "is_official")]
        public bool IsOfficial { get; set; }

        [DataMember(Name = "is_trusted")]
        public bool IsTrusted { get; set; }

        public ImageSearchResponse()
        {
        }
    }
}