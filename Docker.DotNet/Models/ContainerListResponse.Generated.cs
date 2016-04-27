using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerListResponse // (types.Container)
    {
        [DataMember(Name = "Id")]
        public string ID { get; set; }

        [DataMember(Name = "Names")]
        public IList<string> Names { get; set; }

        [DataMember(Name = "Image")]
        public string Image { get; set; }

        [DataMember(Name = "ImageID")]
        public string ImageID { get; set; }

        [DataMember(Name = "Command")]
        public string Command { get; set; }

        [DataMember(Name = "Created")]
        public long Created { get; set; }

        [DataMember(Name = "Ports")]
        public IList<Port> Ports { get; set; }

        [DataMember(Name = "SizeRw")]
        public long SizeRw { get; set; }

        [DataMember(Name = "SizeRootFs")]
        public long SizeRootFs { get; set; }

        [DataMember(Name = "Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [DataMember(Name = "State")]
        public string State { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "NetworkSettings")]
        public SummaryNetworkSettings NetworkSettings { get; set; }

        [DataMember(Name = "Mounts")]
        public IList<MountPoint> Mounts { get; set; }
    }
}
