using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerProcessesResponse // (types.ContainerProcessList)
    {
        [DataMember(Name = "Processes")]
        public IList<IList<string>> Processes { get; set; }

        [DataMember(Name = "Titles")]
        public IList<string> Titles { get; set; }
    }
}
