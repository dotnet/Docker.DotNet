using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerUpdateParameters // (main.ContainerUpdateParameters)
    {
        public ContainerUpdateParameters()
        {
        }

        public ContainerUpdateParameters(UpdateConfig UpdateConfig)
        {
            if (UpdateConfig != null)
            {
                this.CPUShares = UpdateConfig.CPUShares;
                this.Memory = UpdateConfig.Memory;
                this.CgroupParent = UpdateConfig.CgroupParent;
                this.BlkioWeight = UpdateConfig.BlkioWeight;
                this.BlkioWeightDevice = UpdateConfig.BlkioWeightDevice;
                this.BlkioDeviceReadBps = UpdateConfig.BlkioDeviceReadBps;
                this.BlkioDeviceWriteBps = UpdateConfig.BlkioDeviceWriteBps;
                this.BlkioDeviceReadIOps = UpdateConfig.BlkioDeviceReadIOps;
                this.BlkioDeviceWriteIOps = UpdateConfig.BlkioDeviceWriteIOps;
                this.CPUPeriod = UpdateConfig.CPUPeriod;
                this.CPUQuota = UpdateConfig.CPUQuota;
                this.CpusetCpus = UpdateConfig.CpusetCpus;
                this.CpusetMems = UpdateConfig.CpusetMems;
                this.Devices = UpdateConfig.Devices;
                this.DiskQuota = UpdateConfig.DiskQuota;
                this.KernelMemory = UpdateConfig.KernelMemory;
                this.MemoryReservation = UpdateConfig.MemoryReservation;
                this.MemorySwap = UpdateConfig.MemorySwap;
                this.MemorySwappiness = UpdateConfig.MemorySwappiness;
                this.OomKillDisable = UpdateConfig.OomKillDisable;
                this.PidsLimit = UpdateConfig.PidsLimit;
                this.Ulimits = UpdateConfig.Ulimits;
                this.CPUCount = UpdateConfig.CPUCount;
                this.CPUPercent = UpdateConfig.CPUPercent;
                this.IOMaximumIOps = UpdateConfig.IOMaximumIOps;
                this.IOMaximumBandwidth = UpdateConfig.IOMaximumBandwidth;
                this.RestartPolicy = UpdateConfig.RestartPolicy;
            }
        }

        [DataMember(Name = "CpuShares", EmitDefaultValue = false)]
        public long CPUShares { get; set; }

        [DataMember(Name = "Memory", EmitDefaultValue = false)]
        public long Memory { get; set; }

        [DataMember(Name = "CgroupParent", EmitDefaultValue = false)]
        public string CgroupParent { get; set; }

        [DataMember(Name = "BlkioWeight", EmitDefaultValue = false)]
        public ushort BlkioWeight { get; set; }

        [DataMember(Name = "BlkioWeightDevice", EmitDefaultValue = false)]
        public IList<WeightDevice> BlkioWeightDevice { get; set; }

        [DataMember(Name = "BlkioDeviceReadBps", EmitDefaultValue = false)]
        public IList<ThrottleDevice> BlkioDeviceReadBps { get; set; }

        [DataMember(Name = "BlkioDeviceWriteBps", EmitDefaultValue = false)]
        public IList<ThrottleDevice> BlkioDeviceWriteBps { get; set; }

        [DataMember(Name = "BlkioDeviceReadIOps", EmitDefaultValue = false)]
        public IList<ThrottleDevice> BlkioDeviceReadIOps { get; set; }

        [DataMember(Name = "BlkioDeviceWriteIOps", EmitDefaultValue = false)]
        public IList<ThrottleDevice> BlkioDeviceWriteIOps { get; set; }

        [DataMember(Name = "CpuPeriod", EmitDefaultValue = false)]
        public long CPUPeriod { get; set; }

        [DataMember(Name = "CpuQuota", EmitDefaultValue = false)]
        public long CPUQuota { get; set; }

        [DataMember(Name = "CpusetCpus", EmitDefaultValue = false)]
        public string CpusetCpus { get; set; }

        [DataMember(Name = "CpusetMems", EmitDefaultValue = false)]
        public string CpusetMems { get; set; }

        [DataMember(Name = "Devices", EmitDefaultValue = false)]
        public IList<DeviceMapping> Devices { get; set; }

        [DataMember(Name = "DiskQuota", EmitDefaultValue = false)]
        public long DiskQuota { get; set; }

        [DataMember(Name = "KernelMemory", EmitDefaultValue = false)]
        public long KernelMemory { get; set; }

        [DataMember(Name = "MemoryReservation", EmitDefaultValue = false)]
        public long MemoryReservation { get; set; }

        [DataMember(Name = "MemorySwap", EmitDefaultValue = false)]
        public long MemorySwap { get; set; }

        [DataMember(Name = "MemorySwappiness", EmitDefaultValue = false)]
        public long MemorySwappiness { get; set; }

        [DataMember(Name = "OomKillDisable", EmitDefaultValue = false)]
        public bool OomKillDisable { get; set; }

        [DataMember(Name = "PidsLimit", EmitDefaultValue = false)]
        public long PidsLimit { get; set; }

        [DataMember(Name = "Ulimits", EmitDefaultValue = false)]
        public IList<Ulimit> Ulimits { get; set; }

        [DataMember(Name = "CpuCount", EmitDefaultValue = false)]
        public long CPUCount { get; set; }

        [DataMember(Name = "CpuPercent", EmitDefaultValue = false)]
        public long CPUPercent { get; set; }

        [DataMember(Name = "IOMaximumIOps", EmitDefaultValue = false)]
        public ulong IOMaximumIOps { get; set; }

        [DataMember(Name = "IOMaximumBandwidth", EmitDefaultValue = false)]
        public ulong IOMaximumBandwidth { get; set; }

        [DataMember(Name = "RestartPolicy", EmitDefaultValue = false)]
        public RestartPolicy RestartPolicy { get; set; }
    }
}
