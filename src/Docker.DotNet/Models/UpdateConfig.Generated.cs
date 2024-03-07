using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class UpdateConfig // (container.UpdateConfig)
    {
        public UpdateConfig()
        {
        }

        public UpdateConfig(Resources Resources)
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
