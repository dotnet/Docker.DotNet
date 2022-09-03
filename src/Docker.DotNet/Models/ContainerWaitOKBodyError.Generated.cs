using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerWaitOKBodyError // (container.ContainerWaitOKBodyError)
    {
        [DataMember(Name = "Message", EmitDefaultValue = false)]
        public string Message { get; set; }
    }
}
