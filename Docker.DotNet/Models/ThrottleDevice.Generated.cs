using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ThrottleDevice // (blkiodev.ThrottleDevice)
    {
        [DataMember(Name = "Path")]
        public string Path { get; set; }

        [DataMember(Name = "Rate")]
        public ulong Rate { get; set; }
    }
}
