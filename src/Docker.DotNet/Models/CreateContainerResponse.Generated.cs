using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CreateContainerResponse // (container.CreateResponse)
    {
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "Warnings", EmitDefaultValue = false)]
        public IList<string> Warnings { get; set; }
    }
}
