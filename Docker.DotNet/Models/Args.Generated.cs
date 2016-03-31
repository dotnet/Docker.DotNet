using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Args // (filters.Args)
    {
        [DataMember(Name = "fields")]
        public IDictionary<string, IDictionary<string, bool>> fields { get; set; }
    }
}
