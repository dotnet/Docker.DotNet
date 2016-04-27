using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class IndexInfo // (registry.IndexInfo)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Mirrors")]
        public IList<string> Mirrors { get; set; }

        [DataMember(Name = "Secure")]
        public bool Secure { get; set; }

        [DataMember(Name = "Official")]
        public bool Official { get; set; }
    }
}
