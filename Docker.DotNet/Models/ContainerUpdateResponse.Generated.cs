using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerUpdateResponse // (types.ContainerUpdateResponse)
    {
        [DataMember(Name = "Warnings")]
        public IList<string> Warnings { get; set; }
    }
}
