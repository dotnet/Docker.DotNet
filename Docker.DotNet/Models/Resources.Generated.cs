using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class Resources // (container.Resources)
    {
        [DataMember(Name = "CpuShares")]
        public long CPUShares { get; set; }

        [DataMember(Name = "Memory")]
        public long Memory { get; set; }

        [DataMember(Name = "CgroupParent")]
        public string CgroupParent { get; set; }

        [DataMember(Name = "BlkioWeight")]
        public ushort BlkioWeight { get; set; }

        [DataMember(Name = "BlkioWeightDevice")]
        public IList<WeightDevice> BlkioWeightDevice { get; set; }

        [DataMember(Name = "BlkioDeviceReadBps")]
        public IList<ThrottleDevice> BlkioDeviceReadBps { get; set; }

        [DataMember(Name = "BlkioDeviceWriteBps")]
        public IList<ThrottleDevice> BlkioDeviceWriteBps { get; set; }

        [DataMember(Name = "BlkioDeviceReadIOps")]
        public IList<ThrottleDevice> BlkioDeviceReadIOps { get; set; }

        [DataMember(Name = "BlkioDeviceWriteIOps")]
        public IList<ThrottleDevice> BlkioDeviceWriteIOps { get; set; }

        [DataMember(Name = "CpuPeriod")]
        public long CPUPeriod { get; set; }

        [DataMember(Name = "CpuQuota")]
        public long CPUQuota { get; set; }

        [DataMember(Name = "CpusetCpus")]
        public string CpusetCpus { get; set; }

        [DataMember(Name = "CpusetMems")]
        public string CpusetMems { get; set; }

        [DataMember(Name = "Devices")]
        public IList<DeviceMapping> Devices { get; set; }

        [DataMember(Name = "DiskQuota")]
        public long DiskQuota { get; set; }

        [DataMember(Name = "KernelMemory")]
        public long KernelMemory { get; set; }

        [DataMember(Name = "MemoryReservation")]
        public long MemoryReservation { get; set; }

        [DataMember(Name = "MemorySwap")]
        public long MemorySwap { get; set; }

        [DataMember(Name = "MemorySwappiness")]
        public long MemorySwappiness { get; set; }

        [DataMember(Name = "OomKillDisable")]
        public bool OomKillDisable { get; set; }

        [DataMember(Name = "PidsLimit")]
        public long PidsLimit { get; set; }

        [DataMember(Name = "Ulimits")]
        public IList<Ulimit> Ulimits { get; set; }

        [DataMember(Name = "CpuCount")]
        public long CPUCount { get; set; }

        [DataMember(Name = "CpuPercent")]
        public long CPUPercent { get; set; }

        [DataMember(Name = "IOMaximumIOps")]
        public ulong IOMaximumIOps { get; set; }

        [DataMember(Name = "IOMaximumBandwidth")]
        public ulong IOMaximumBandwidth { get; set; }

        [DataMember(Name = "NetworkMaximumBandwidth")]
        public ulong NetworkMaximumBandwidth { get; set; }
    }
}
