using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CreateContainerResponse // (types.ContainerCreateResponse)
    {
        [DataMember(Name = "Id")]
        public string ID { get; set; }

        [DataMember(Name = "Warnings")]
        public IList<string> Warnings { get; set; }
    }
}
