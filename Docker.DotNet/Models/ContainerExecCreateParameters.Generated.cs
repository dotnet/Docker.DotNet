using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerExecCreateParameters // (types.ExecConfig)
    {
        [DataMember(Name = "User")]
        public string User { get; set; }

        [DataMember(Name = "Privileged")]
        public bool Privileged { get; set; }

        [DataMember(Name = "Tty")]
        public bool Tty { get; set; }

        [DataMember(Name = "Container")]
        public string Container { get; set; }

        [DataMember(Name = "AttachStdin")]
        public bool AttachStdin { get; set; }

        [DataMember(Name = "AttachStderr")]
        public bool AttachStderr { get; set; }

        [DataMember(Name = "AttachStdout")]
        public bool AttachStdout { get; set; }

        [DataMember(Name = "Detach")]
        public bool Detach { get; set; }

        [DataMember(Name = "DetachKeys")]
        public string DetachKeys { get; set; }

        [DataMember(Name = "Cmd")]
        public IList<string> Cmd { get; set; }
    }
}
