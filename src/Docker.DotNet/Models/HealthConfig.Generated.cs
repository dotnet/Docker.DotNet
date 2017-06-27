using System;
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
        [TimeSpanSerialization(SerializationTarget.Nanoseconds)]
        public TimeSpan Interval { get; set; }

        [DataMember(Name = "Timeout", EmitDefaultValue = false)]
        [TimeSpanSerialization(SerializationTarget.Nanoseconds)]
        public TimeSpan Timeout { get; set; }

        [DataMember(Name = "Retries", EmitDefaultValue = false)]
        public long Retries { get; set; }
    }
}
