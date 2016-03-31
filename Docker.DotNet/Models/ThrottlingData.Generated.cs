using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ThrottlingData // (types.ThrottlingData)
    {
        [DataMember(Name = "periods")]
        public ulong Periods { get; set; }

        [DataMember(Name = "throttled_periods")]
        public ulong ThrottledPeriods { get; set; }

        [DataMember(Name = "throttled_time")]
        public ulong ThrottledTime { get; set; }
    }
}
