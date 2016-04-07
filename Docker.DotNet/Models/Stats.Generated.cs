using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Stats // (types.Stats)
    {
        [DataMember(Name = "read")]
        public DateTime Read { get; set; }

        [DataMember(Name = "precpu_stats")]
        public CPUStats PreCPUStats { get; set; }

        [DataMember(Name = "cpu_stats")]
        public CPUStats CPUStats { get; set; }

        [DataMember(Name = "memory_stats")]
        public MemoryStats MemoryStats { get; set; }

        [DataMember(Name = "blkio_stats")]
        public BlkioStats BlkioStats { get; set; }

        [DataMember(Name = "pids_stats")]
        public PidsStats PidsStats { get; set; }
    }
}
