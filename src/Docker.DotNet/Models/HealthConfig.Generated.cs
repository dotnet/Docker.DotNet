using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class HealthConfig // (container.HealthConfig)
    {
        [DataMember(Name = "Test", EmitDefaultValue = false)]
        public IList<string> Test { get; set; }

        [DataMember(Name = "Interval", EmitDefaultValue = false)]
        public long Interval { get; set; }

        [DataMember(Name = "Timeout", EmitDefaultValue = false)]
        public long Timeout { get; set; }

        [DataMember(Name = "Retries", EmitDefaultValue = false)]
        public long Retries { get; set; }
    }
}
