using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Ulimit // (units.Ulimit)
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Hard")]
        public long Hard { get; set; }

        [DataMember(Name = "Soft")]
        public long Soft { get; set; }
    }
}
