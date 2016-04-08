using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageSearchResponse // (registry.SearchResult)
    {
        [DataMember(Name = "star_count")]
        public int StarCount { get; set; }

        [DataMember(Name = "is_official")]
        public bool IsOfficial { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "is_trusted")]
        public bool IsTrusted { get; set; }

        [DataMember(Name = "is_automated")]
        public bool IsAutomated { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
