using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class RootFS // (types.RootFS)
    {
        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "Layers")]
        public IList<string> Layers { get; set; }

        [DataMember(Name = "BaseLayer")]
        public string BaseLayer { get; set; }
    }
}
