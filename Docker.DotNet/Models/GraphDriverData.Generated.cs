using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class GraphDriverData // (types.GraphDriverData)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Data")]
        public IDictionary<string, string> Data { get; set; }
    }
}
