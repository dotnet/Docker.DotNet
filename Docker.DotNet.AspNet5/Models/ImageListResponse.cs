using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageListResponse
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "ParentId")]
        public string ParentId { get; set; }

        [DataMember(Name = "RepoTags")]
        public IList<string> RepoTags { get; set; }

        [DataMember(Name = "Created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "Size")]
        public long Size { get; set; }

        [DataMember(Name = "VirtualSize")]
        public long VirtualSize { get; set; }

        public ImageListResponse()
        {
        }
    }
}