using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImagesListResponse // (types.Image)
    {
        [DataMember(Name = "Id")]
        public string ID { get; set; }

        [DataMember(Name = "ParentId")]
        public string ParentID { get; set; }

        [DataMember(Name = "RepoTags")]
        public IList<string> RepoTags { get; set; }

        [DataMember(Name = "RepoDigests")]
        public IList<string> RepoDigests { get; set; }

        [DataMember(Name = "Created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "Size")]
        public long Size { get; set; }

        [DataMember(Name = "VirtualSize")]
        public long VirtualSize { get; set; }

        [DataMember(Name = "Labels")]
        public IDictionary<string, string> Labels { get; set; }
    }
}
