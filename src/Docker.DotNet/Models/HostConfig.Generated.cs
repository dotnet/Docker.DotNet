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
                this.NanoCPUs = Resources.NanoCPUs;
                this.CgroupParent = Resources.CgroupParent;
                this.BlkioWeight = Resources.BlkioWeight;
                this.BlkioWeightDevice = Resources.BlkioWeightDevice;
                this.BlkioDeviceReadBps = Resources.BlkioDeviceReadBps;
                this.BlkioDeviceWriteBps = Resources.BlkioDeviceWriteBps;
                this.BlkioDeviceReadIOps = Resources.BlkioDeviceReadIOps;
                this.BlkioDeviceWriteIOps = Resources.BlkioDeviceWriteIOps;
                this.CPUPeriod = Resources.CPUPeriod;
                this.CPUQuota = Resources.CPUQuota;
                this.CPURealtimePeriod = Resources.CPURealtimePeriod;
                this.CPURealtimeRuntime = Resources.CPURealtimeRuntime;
                this.CpusetCpus = Resources.CpusetCpus;
                this.CpusetMems = Resources.CpusetMems;
                this.Devices = Resources.Devices;
                this.DeviceCgroupRules = Resources.DeviceCgroupRules;
                this.DeviceRequests = Resources.DeviceRequests;
                this.KernelMemory = Resources.KernelMemory;
                this.KernelMemoryTCP = Resources.KernelMemoryTCP;
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
            }
        }

        [DataMember(Name = "Binds", EmitDefaultValue = false)]
        public IList<string> Binds { get; set; }

        [DataMember(Name = "ContainerIDFile", EmitDefaultValue = false)]
        public string ContainerIDFile { get; set; }

        [DataMember(Name = "LogConfig", EmitDefaultValue = false)]
        public LogConfig LogConfig { get; set; }

        [DataMember(Name = "NetworkMode", EmitDefaultValue = false)]
        public string NetworkMode { get; set; }

        [DataMember(Name = "PortBindings", EmitDefaultValue = false)]
        public IDictionary<string, IList<PortBinding>> PortBindings { get; set; }

        [DataMember(Name = "RestartPolicy", EmitDefaultValue = false)]
        public RestartPolicy RestartPolicy { get; set; }

        [DataMember(Name = "AutoRemove", EmitDefaultValue = false)]
        public bool AutoRemove { get; set; }

        [DataMember(Name = "VolumeDriver", EmitDefaultValue = false)]
        public string VolumeDriver { get; set; }

        [DataMember(Name = "VolumesFrom", EmitDefaultValue = false)]
        public IList<string> VolumesFrom { get; set; }

        [DataMember(Name = "CapAdd", EmitDefaultValue = false)]
        public IList<string> CapAdd { get; set; }

        [DataMember(Name = "CapDrop", EmitDefaultValue = false)]
        public IList<string> CapDrop { get; set; }

        [DataMember(Name = "CgroupnsMode", EmitDefaultValue = false)]
        public string CgroupnsMode { get; set; }

        [DataMember(Name = "Dns", EmitDefaultValue = false)]
        public IList<string> DNS { get; set; }

        [DataMember(Name = "DnsOptions", EmitDefaultValue = false)]
        public IList<string> DNSOptions { get; set; }

        [DataMember(Name = "DnsSearch", EmitDefaultValue = false)]
        public IList<string> DNSSearch { get; set; }

        [DataMember(Name = "ExtraHosts", EmitDefaultValue = false)]
        public IList<string> ExtraHosts { get; set; }

        [DataMember(Name = "GroupAdd", EmitDefaultValue = false)]
        public IList<string> GroupAdd { get; set; }

        [DataMember(Name = "IpcMode", EmitDefaultValue = false)]
        public string IpcMode { get; set; }

        [DataMember(Name = "Cgroup", EmitDefaultValue = false)]
        public string Cgroup { get; set; }

        [DataMember(Name = "Links", EmitDefaultValue = false)]
        public IList<string> Links { get; set; }

        [DataMember(Name = "OomScoreAdj", EmitDefaultValue = false)]
        public long OomScoreAdj { get; set; }

        [DataMember(Name = "PidMode", EmitDefaultValue = false)]
        public string PidMode { get; set; }

        [DataMember(Name = "Privileged", EmitDefaultValue = false)]
        public bool Privileged { get; set; }

        [DataMember(Name = "PublishAllPorts", EmitDefaultValue = false)]
        public bool PublishAllPorts { get; set; }

        [DataMember(Name = "ReadonlyRootfs", EmitDefaultValue = false)]
        public bool ReadonlyRootfs { get; set; }

        [DataMember(Name = "SecurityOpt", EmitDefaultValue = false)]
        public IList<string> SecurityOpt { get; set; }

        [DataMember(Name = "StorageOpt", EmitDefaultValue = false)]
        public IDictionary<string, string> StorageOpt { get; set; }

        [DataMember(Name = "Tmpfs", EmitDefaultValue = false)]
        public IDictionary<string, string> Tmpfs { get; set; }

        [DataMember(Name = "UTSMode", EmitDefaultValue = false)]
        public string UTSMode { get; set; }

        [DataMember(Name = "UsernsMode", EmitDefaultValue = false)]
        public string UsernsMode { get; set; }

        [DataMember(Name = "ShmSize", EmitDefaultValue = false)]
        public long ShmSize { get; set; }

        [DataMember(Name = "Sysctls", EmitDefaultValue = false)]
        public IDictionary<string, string> Sysctls { get; set; }

        [DataMember(Name = "Runtime", EmitDefaultValue = false)]
        public string Runtime { get; set; }

        [DataMember(Name = "ConsoleSize", EmitDefaultValue = false)]
        public ulong[] ConsoleSize { get; set; }

        [DataMember(Name = "Isolation", EmitDefaultValue = false)]
        public string Isolation { get; set; }

        [DataMember(Name = "CpuShares", EmitDefaultValue = false)]
        public long CPUShares { get; set; }

        [DataMember(Name = "Memory", EmitDefaultValue = false)]
        public long Memory { get; set; }

        [DataMember(Name = "NanoCpus", EmitDefaultValue = false)]
        public long NanoCPUs { get; set; }

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

        [DataMember(Name = "CpuRealtimePeriod", EmitDefaultValue = false)]
        public long CPURealtimePeriod { get; set; }

        [DataMember(Name = "CpuRealtimeRuntime", EmitDefaultValue = false)]
        public long CPURealtimeRuntime { get; set; }

        [DataMember(Name = "CpusetCpus", EmitDefaultValue = false)]
        public string CpusetCpus { get; set; }

        [DataMember(Name = "CpusetMems", EmitDefaultValue = false)]
        public string CpusetMems { get; set; }

        [DataMember(Name = "Devices", EmitDefaultValue = false)]
        public IList<DeviceMapping> Devices { get; set; }

        [DataMember(Name = "DeviceCgroupRules", EmitDefaultValue = false)]
        public IList<string> DeviceCgroupRules { get; set; }

        [DataMember(Name = "DeviceRequests", EmitDefaultValue = false)]
        public IList<DeviceRequest> DeviceRequests { get; set; }

        [DataMember(Name = "KernelMemory", EmitDefaultValue = false)]
        public long KernelMemory { get; set; }

        [DataMember(Name = "KernelMemoryTCP", EmitDefaultValue = false)]
        public long KernelMemoryTCP { get; set; }

        [DataMember(Name = "MemoryReservation", EmitDefaultValue = false)]
        public long MemoryReservation { get; set; }

        [DataMember(Name = "MemorySwap", EmitDefaultValue = false)]
        public long MemorySwap { get; set; }

        [DataMember(Name = "MemorySwappiness", EmitDefaultValue = false)]
        public long? MemorySwappiness { get; set; }

        [DataMember(Name = "OomKillDisable", EmitDefaultValue = false)]
        public bool? OomKillDisable { get; set; }

        [DataMember(Name = "PidsLimit", EmitDefaultValue = false)]
        public long? PidsLimit { get; set; }

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

        [DataMember(Name = "Mounts", EmitDefaultValue = false)]
        public IList<Mount> Mounts { get; set; }

        [DataMember(Name = "MaskedPaths", EmitDefaultValue = false)]
        public IList<string> MaskedPaths { get; set; }

        [DataMember(Name = "ReadonlyPaths", EmitDefaultValue = false)]
        public IList<string> ReadonlyPaths { get; set; }

        [DataMember(Name = "Init", EmitDefaultValue = false)]
        public bool? Init { get; set; }
    }
}
