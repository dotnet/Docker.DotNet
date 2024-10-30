using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
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
                this.NanoCPUs = UpdateConfig.NanoCPUs;
                this.CgroupParent = UpdateConfig.CgroupParent;
                this.BlkioWeight = UpdateConfig.BlkioWeight;
                this.BlkioWeightDevice = UpdateConfig.BlkioWeightDevice;
                this.BlkioDeviceReadBps = UpdateConfig.BlkioDeviceReadBps;
                this.BlkioDeviceWriteBps = UpdateConfig.BlkioDeviceWriteBps;
                this.BlkioDeviceReadIOps = UpdateConfig.BlkioDeviceReadIOps;
                this.BlkioDeviceWriteIOps = UpdateConfig.BlkioDeviceWriteIOps;
                this.CPUPeriod = UpdateConfig.CPUPeriod;
                this.CPUQuota = UpdateConfig.CPUQuota;
                this.CPURealtimePeriod = UpdateConfig.CPURealtimePeriod;
                this.CPURealtimeRuntime = UpdateConfig.CPURealtimeRuntime;
                this.CpusetCpus = UpdateConfig.CpusetCpus;
                this.CpusetMems = UpdateConfig.CpusetMems;
                this.Devices = UpdateConfig.Devices;
                this.DeviceCgroupRules = UpdateConfig.DeviceCgroupRules;
                this.DeviceRequests = UpdateConfig.DeviceRequests;
                this.KernelMemory = UpdateConfig.KernelMemory;
                this.KernelMemoryTCP = UpdateConfig.KernelMemoryTCP;
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

        [JsonPropertyName("RestartPolicy")]
        public RestartPolicy RestartPolicy { get; set; }
    }
}
