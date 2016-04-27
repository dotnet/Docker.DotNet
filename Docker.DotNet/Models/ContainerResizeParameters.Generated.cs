using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerResizeParameters // (types.ResizeOptions)
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [QueryStringParameter("h", false)]
        public int? Height { get; set; }

        [QueryStringParameter("w", false)]
        public int? Width { get; set; }
    }
}
