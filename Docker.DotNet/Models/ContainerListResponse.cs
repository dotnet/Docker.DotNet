using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerListResponse
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Names")]
        public IList<string> Names { get; set; }

        [DataMember(Name = "Image")]
        public string Image { get; set; }

        [DataMember(Name = "Created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "Command")]
        public string Command { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "SizeRw")]
        public long SizeRw { get; set; }

        [DataMember(Name = "SizeRootFs")]
        public long SizeRootFs { get; set; }

        [DataMember(Name = "Ports")]
        public IList<Port> Ports { get; set; }


        public ContainerListResponse()
        {
        }
    }
}