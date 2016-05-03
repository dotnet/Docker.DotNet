using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class RestartPolicy // (container.RestartPolicy)
    {
        [DataMember(Name = "Name")]
        public RestartPolicyKind Name { get; set; }

        [DataMember(Name = "MaximumRetryCount")]
        public long MaximumRetryCount { get; set; }
    }
}
