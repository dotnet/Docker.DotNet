using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CommitContainerChangesResponse // (types.ContainerCommitResponse)
    {
        [DataMember(Name = "Id", EmitDefaultValue = false)]
        public string ID { get; set; }
    }
}
