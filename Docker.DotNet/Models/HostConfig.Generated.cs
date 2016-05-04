using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class HostConfig // (container.HostConfig)
    {
        public HostConfig()
        {
        }

        public HostConfig(Resources Resources)
        {
            if (Resources != null)
            {
                this.CPUShares = Resources.CPUShares;
                this.Memory = Resources.Memory;
                this.CgroupParent = Resources.CgroupParent;
                this.BlkioWeight = Resources.BlkioWeight;
                this.BlkioWeightDevice = Resources.BlkioWeightDevice;
                this.BlkioDeviceReadBps = Resources.BlkioDeviceReadBps;
                this.BlkioDeviceWriteBps = Resources.BlkioDeviceWriteBps;
                this.BlkioDeviceReadIOps = Resources.BlkioDeviceReadIOps;
                this.BlkioDeviceWriteIOps = Resources.BlkioDeviceWriteIOps;
                this.CPUPeriod = Resources.CPUPeriod;
                this.CPUQuota = Resources.CPUQuota;
                this.CpusetCpus = Resources.CpusetCpus;
                this.CpusetMems = Resources.CpusetMems;
                this.Devices = Resources.Devices;
                this.DiskQuota = Resources.DiskQuota;
                this.KernelMemory = Resources.KernelMemory;
                this.MemoryReservation = Resources.MemoryReservation;
                this.MemorySwap = Resources.MemorySwap;
                this.MemorySwappiness = Resources.MemorySwappiness;
                this.OomKillDisable = Resources.OomKillDisable;
                this.PidsLimit = Resources.PidsLimit;
                this.Ulimits = Resources.Ulimits;
                this.CPUCount = Resources.CPUCount;
                this.CPUPercent = Resources.CPUPercent;
                this.IOMaximumIOps = Resources.IOMaximumIOps;
                this.IOMaximumBandwidth = Resources.IOMaximumBandwidth;
                this.NetworkMaximumBandwidth = Resources.NetworkMaximumBandwidth;
            }
        }

        [DataMember(Name = "Binds")]
        public IList<string> Binds { get; set; }

        [DataMember(Name = "ContainerIDFile")]
        public string ContainerIDFile { get; set; }

        [DataMember(Name = "LogConfig")]
        public LogConfig LogConfig { get; set; }

        [DataMember(Name = "NetworkMode")]
        public string NetworkMode { get; set; }

        [DataMember(Name = "PortBindings")]
        public IDictionary<string, IList<PortBinding>> PortBindings { get; set; }

        [DataMember(Name = "RestartPolicy")]
        public RestartPolicy RestartPolicy { get; set; }

        [DataMember(Name = "AutoRemove")]
        public bool AutoRemove { get; set; }

        [DataMember(Name = "VolumeDriver")]
        public string VolumeDriver { get; set; }

        [DataMember(Name = "VolumesFrom")]
        public IList<string> VolumesFrom { get; set; }

        [DataMember(Name = "CapAdd")]
        public IList<string> CapAdd { get; set; }

        [DataMember(Name = "CapDrop")]
        public IList<string> CapDrop { get; set; }

        [DataMember(Name = "Dns")]
        public IList<string> DNS { get; set; }

        [DataMember(Name = "DnsOptions")]
        public IList<string> DNSOptions { get; set; }

        [DataMember(Name = "DnsSearch")]
        public IList<string> DNSSearch { get; set; }

        [DataMember(Name = "ExtraHosts")]
        public IList<string> ExtraHosts { get; set; }

        [DataMember(Name = "GroupAdd")]
        public IList<string> GroupAdd { get; set; }

        [DataMember(Name = "IpcMode")]
        public string IpcMode { get; set; }

        [DataMember(Name = "Cgroup")]
        public string Cgroup { get; set; }

        [DataMember(Name = "Links")]
        public IList<string> Links { get; set; }

        [DataMember(Name = "OomScoreAdj")]
        public long OomScoreAdj { get; set; }

        [DataMember(Name = "PidMode")]
        public string PidMode { get; set; }

        [DataMember(Name = "Privileged")]
        public bool Privileged { get; set; }

        [DataMember(Name = "PublishAllPorts")]
        public bool PublishAllPorts { get; set; }

        [DataMember(Name = "ReadonlyRootfs")]
        public bool ReadonlyRootfs { get; set; }

        [DataMember(Name = "SecurityOpt")]
        public IList<string> SecurityOpt { get; set; }

        [DataMember(Name = "StorageOpt")]
        public IDictionary<string, string> StorageOpt { get; set; }

        [DataMember(Name = "Tmpfs")]
        public IDictionary<string, string> Tmpfs { get; set; }

        [DataMember(Name = "UTSMode")]
        public string UTSMode { get; set; }

        [DataMember(Name = "UsernsMode")]
        public string UsernsMode { get; set; }

        [DataMember(Name = "ShmSize")]
        public long ShmSize { get; set; }

        [DataMember(Name = "Sysctls")]
        public IDictionary<string, string> Sysctls { get; set; }

        [DataMember(Name = "ConsoleSize")]
        public long[] ConsoleSize { get; set; }

        [DataMember(Name = "Isolation")]
        public string Isolation { get; set; }

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
