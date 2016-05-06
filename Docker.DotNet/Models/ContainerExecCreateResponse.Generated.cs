using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerExecCreateResponse // (types.ContainerExecCreateResponse)
    {
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string ID { get; set; }
    }
}
