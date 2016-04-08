using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerRemoveParameters // (types.ContainerRemoveOptions)
    {
        [DataMember(Name = "ContainerID")]
        public string ContainerID { get; set; }

        [QueryStringParameter("v", false, typeof(BoolQueryStringConverter))]
        public bool? RemoveVolumes { get; set; }

        [DataMember(Name = "RemoveLinks")]
        public bool RemoveLinks { get; set; }

        [QueryStringParameter("force", false, typeof(BoolQueryStringConverter))]
        public bool? Force { get; set; }
    }
}
