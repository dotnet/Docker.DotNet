using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainersListParameters // (types.ContainerListOptions)
    {
        [DataMember(Name = "Quiet")]
        public bool Quiet { get; set; }

        [QueryStringParameter("size", false, typeof(BoolQueryStringConverter))]
        public bool? Size { get; set; }

        [QueryStringParameter("all", false, typeof(BoolQueryStringConverter))]
        public bool? All { get; set; }

        [DataMember(Name = "Latest")]
        public bool Latest { get; set; }

        [QueryStringParameter("since", false)]
        public string Since { get; set; }

        [QueryStringParameter("before", false)]
        public string Before { get; set; }

        [QueryStringParameter("limit", false)]
        public int? Limit { get; set; }

        [QueryStringParameter("filters", false)]
        public Args Filter { get; set; }
    }
}
