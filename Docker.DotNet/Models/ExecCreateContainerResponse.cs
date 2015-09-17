using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ExecCreateContainerResponse
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Warnings")]
        public IList<string> Warnings { get; set; }

        public ExecCreateContainerResponse()
        {
        }
    }
}
