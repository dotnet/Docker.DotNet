using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerExecStartParameters // (types.ExecStartCheck)
    {
        [DataMember(Name = "Detach", EmitDefaultValue = false)]
        public bool Detach { get; set; }

        [DataMember(Name = "Tty", EmitDefaultValue = false)]
        public bool Tty { get; set; }
    }
}
