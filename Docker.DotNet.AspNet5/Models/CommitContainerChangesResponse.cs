using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CommitContainerChangesResponse
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        public CommitContainerChangesResponse()
        {
        }
    }
}