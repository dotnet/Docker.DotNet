using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
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

        [JsonPropertyName("Binds")]
        public IList<string> Binds { get; set; }

        [JsonPropertyName("ContainerIDFile")]
        public string ContainerIDFile { get; set; }

        [JsonPropertyName("LogConfig")]
        public LogConfig LogConfig { get; set; }

        [JsonPropertyName("NetworkMode")]
        public string NetworkMode { get; set; }

        [JsonPropertyName("PortBindings")]
        public IDictionary<string, IList<PortBinding>> PortBindings { get; set; }

        [JsonPropertyName("RestartPolicy")]
        public RestartPolicy RestartPolicy { get; set; }

        [JsonPropertyName("AutoRemove")]
        public bool AutoRemove { get; set; }

        [JsonPropertyName("VolumeDriver")]
        public string VolumeDriver { get; set; }

        [JsonPropertyName("VolumesFrom")]
        public IList<string> VolumesFrom { get; set; }

        [JsonPropertyName("ConsoleSize")]
        public ulong[] ConsoleSize { get; set; }

        [JsonPropertyName("Annotations")]
        public IDictionary<string, string> Annotations { get; set; }

        [JsonPropertyName("CapAdd")]
        public IList<string> CapAdd { get; set; }

        [JsonPropertyName("CapDrop")]
        public IList<string> CapDrop { get; set; }

        [JsonPropertyName("CgroupnsMode")]
        public string CgroupnsMode { get; set; }

        [JsonPropertyName("Dns")]
        public IList<string> DNS { get; set; }

        [JsonPropertyName("DnsOptions")]
        public IList<string> DNSOptions { get; set; }

        [JsonPropertyName("DnsSearch")]
        public IList<string> DNSSearch { get; set; }

        [JsonPropertyName("ExtraHosts")]
        public IList<string> ExtraHosts { get; set; }

        [JsonPropertyName("GroupAdd")]
        public IList<string> GroupAdd { get; set; }

        [JsonPropertyName("IpcMode")]
        public string IpcMode { get; set; }

        [JsonPropertyName("Cgroup")]
        public string Cgroup { get; set; }

        [JsonPropertyName("Links")]
        public IList<string> Links { get; set; }

        [JsonPropertyName("OomScoreAdj")]
        public long OomScoreAdj { get; set; }

        [JsonPropertyName("PidMode")]
        public string PidMode { get; set; }

        [JsonPropertyName("Privileged")]
        public bool Privileged { get; set; }

        [JsonPropertyName("PublishAllPorts")]
        public bool PublishAllPorts { get; set; }

        [JsonPropertyName("ReadonlyRootfs")]
        public bool ReadonlyRootfs { get; set; }

        [JsonPropertyName("SecurityOpt")]
        public IList<string> SecurityOpt { get; set; }

        [JsonPropertyName("StorageOpt")]
        public IDictionary<string, string> StorageOpt { get; set; }

        [JsonPropertyName("Tmpfs")]
        public IDictionary<string, string> Tmpfs { get; set; }

        [JsonPropertyName("UTSMode")]
        public string UTSMode { get; set; }

        [JsonPropertyName("UsernsMode")]
        public string UsernsMode { get; set; }

        [JsonPropertyName("ShmSize")]
        public long ShmSize { get; set; }

        [JsonPropertyName("Sysctls")]
        public IDictionary<string, string> Sysctls { get; set; }

        [JsonPropertyName("Runtime")]
        public string Runtime { get; set; }

        [JsonPropertyName("Isolation")]
        public string Isolation { get; set; }

        [JsonPropertyName("CpuShares")]
        public long CPUShares { get; set; }

        [JsonPropertyName("Memory")]
        public long Memory { get; set; }

        [JsonPropertyName("NanoCpus")]
        public long NanoCPUs { get; set; }

        [JsonPropertyName("CgroupParent")]
        public string CgroupParent { get; set; }

        [JsonPropertyName("BlkioWeight")]
        public ushort BlkioWeight { get; set; }

        [JsonPropertyName("BlkioWeightDevice")]
        public IList<WeightDevice> BlkioWeightDevice { get; set; }

        [JsonPropertyName("BlkioDeviceReadBps")]
        public IList<ThrottleDevice> BlkioDeviceReadBps { get; set; }

        [JsonPropertyName("BlkioDeviceWriteBps")]
        public IList<ThrottleDevice> BlkioDeviceWriteBps { get; set; }

        [JsonPropertyName("BlkioDeviceReadIOps")]
        public IList<ThrottleDevice> BlkioDeviceReadIOps { get; set; }

        [JsonPropertyName("BlkioDeviceWriteIOps")]
        public IList<ThrottleDevice> BlkioDeviceWriteIOps { get; set; }

        [JsonPropertyName("CpuPeriod")]
        public long CPUPeriod { get; set; }

        [JsonPropertyName("CpuQuota")]
        public long CPUQuota { get; set; }

        [JsonPropertyName("CpuRealtimePeriod")]
        public long CPURealtimePeriod { get; set; }

        [JsonPropertyName("CpuRealtimeRuntime")]
        public long CPURealtimeRuntime { get; set; }

        [JsonPropertyName("CpusetCpus")]
        public string CpusetCpus { get; set; }

        [JsonPropertyName("CpusetMems")]
        public string CpusetMems { get; set; }

        [JsonPropertyName("Devices")]
        public IList<DeviceMapping> Devices { get; set; }

        [JsonPropertyName("DeviceCgroupRules")]
        public IList<string> DeviceCgroupRules { get; set; }

        [JsonPropertyName("DeviceRequests")]
        public IList<DeviceRequest> DeviceRequests { get; set; }

        [JsonPropertyName("KernelMemory")]
        public long KernelMemory { get; set; }

        [JsonPropertyName("KernelMemoryTCP")]
        public long KernelMemoryTCP { get; set; }

        [JsonPropertyName("MemoryReservation")]
        public long MemoryReservation { get; set; }

        [JsonPropertyName("MemorySwap")]
        public long MemorySwap { get; set; }

        [JsonPropertyName("MemorySwappiness")]
        public long? MemorySwappiness { get; set; }

        [JsonPropertyName("OomKillDisable")]
        public bool? OomKillDisable { get; set; }

        [JsonPropertyName("PidsLimit")]
        public long? PidsLimit { get; set; }

        [JsonPropertyName("Ulimits")]
        public IList<Ulimit> Ulimits { get; set; }

        [JsonPropertyName("CpuCount")]
        public long CPUCount { get; set; }

        [JsonPropertyName("CpuPercent")]
        public long CPUPercent { get; set; }

        [JsonPropertyName("IOMaximumIOps")]
        public ulong IOMaximumIOps { get; set; }

        [JsonPropertyName("IOMaximumBandwidth")]
        public ulong IOMaximumBandwidth { get; set; }

        [JsonPropertyName("Mounts")]
        public IList<Mount> Mounts { get; set; }

        [JsonPropertyName("MaskedPaths")]
        public IList<string> MaskedPaths { get; set; }

        [JsonPropertyName("ReadonlyPaths")]
        public IList<string> ReadonlyPaths { get; set; }

        [JsonPropertyName("Init")]
        public bool? Init { get; set; }
    }
}
