using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerWaitResponse // (main.ContainerWaitResponse)
    {
        [DataMember(Name = "Error", EmitDefaultValue = false)]
        public ContainerWaitOKBodyError Error { get; set; }

        [DataMember(Name = "StatusCode", EmitDefaultValue = false)]
        public long StatusCode { get; set; }
    }
}
