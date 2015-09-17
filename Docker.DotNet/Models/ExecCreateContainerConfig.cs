using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    public class ExecCreateContainerConfig
    {
        [DataMember(Name = "AttachStdin")]
        public bool ? AttachStdin { get; set; }

        [DataMember(Name = "AttachStdout")]
        public bool ? AttachStdout { get; set; }

        [DataMember(Name = "AttachStderr")]
        public bool ? AttachStderr { get; set; }

        [DataMember(Name = "Tty")]
        public bool ? Tty { get; set; }

        [DataMember(Name = "Cmd")]
        public IList<string> Cmd { get; set; }
    }
}
