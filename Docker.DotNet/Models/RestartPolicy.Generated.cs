using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class RestartPolicy // (container.RestartPolicy)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "MaximumRetryCount")]
        public int MaximumRetryCount { get; set; }
    }
}
