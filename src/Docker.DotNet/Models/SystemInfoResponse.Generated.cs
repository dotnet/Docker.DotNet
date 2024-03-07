using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SystemInfoResponse // (types.Info)
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("Containers")]
        public long Containers { get; set; }

        [JsonPropertyName("ContainersRunning")]
        public long ContainersRunning { get; set; }

        [JsonPropertyName("ContainersPaused")]
        public long ContainersPaused { get; set; }

        [JsonPropertyName("ContainersStopped")]
        public long ContainersStopped { get; set; }

        [JsonPropertyName("Images")]
        public long Images { get; set; }

        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

        [JsonPropertyName("DriverStatus")]
        public IList<string[]> DriverStatus { get; set; }

        [JsonPropertyName("SystemStatus")]
        public IList<string[]> SystemStatus { get; set; }

        [JsonPropertyName("Plugins")]
        public PluginsInfo Plugins { get; set; }

        [JsonPropertyName("MemoryLimit")]
        public bool MemoryLimit { get; set; }

        [JsonPropertyName("SwapLimit")]
        public bool SwapLimit { get; set; }

        [JsonPropertyName("KernelMemory")]
        public bool KernelMemory { get; set; }

        [JsonPropertyName("KernelMemoryTCP")]
        public bool KernelMemoryTCP { get; set; }

        [JsonPropertyName("CpuCfsPeriod")]
        public bool CPUCfsPeriod { get; set; }

        [JsonPropertyName("CpuCfsQuota")]
        public bool CPUCfsQuota { get; set; }

        [JsonPropertyName("CPUShares")]
        public bool CPUShares { get; set; }

        [JsonPropertyName("CPUSet")]
        public bool CPUSet { get; set; }

        [JsonPropertyName("PidsLimit")]
        public bool PidsLimit { get; set; }

        [JsonPropertyName("IPv4Forwarding")]
        public bool IPv4Forwarding { get; set; }

        [JsonPropertyName("BridgeNfIptables")]
        public bool BridgeNfIptables { get; set; }

        [JsonPropertyName("BridgeNfIp6tables")]
        public bool BridgeNfIP6tables { get; set; }

        [JsonPropertyName("Debug")]
        public bool Debug { get; set; }

        [JsonPropertyName("NFd")]
        public long NFd { get; set; }

        [JsonPropertyName("OomKillDisable")]
        public bool OomKillDisable { get; set; }

        [JsonPropertyName("NGoroutines")]
        public long NGoroutines { get; set; }

        [JsonPropertyName("SystemTime")]
        public string SystemTime { get; set; }

        [JsonPropertyName("LoggingDriver")]
        public string LoggingDriver { get; set; }

        [JsonPropertyName("CgroupDriver")]
        public string CgroupDriver { get; set; }

        [JsonPropertyName("CgroupVersion")]
        public string CgroupVersion { get; set; }

        [JsonPropertyName("NEventsListener")]
        public long NEventsListener { get; set; }

        [JsonPropertyName("KernelVersion")]
        public string KernelVersion { get; set; }

        [JsonPropertyName("OperatingSystem")]
        public string OperatingSystem { get; set; }

        [JsonPropertyName("OSVersion")]
        public string OSVersion { get; set; }

        [JsonPropertyName("OSType")]
        public string OSType { get; set; }

        [JsonPropertyName("Architecture")]
        public string Architecture { get; set; }

        [JsonPropertyName("IndexServerAddress")]
        public string IndexServerAddress { get; set; }

        [JsonPropertyName("RegistryConfig")]
        public ServiceConfig RegistryConfig { get; set; }

        [JsonPropertyName("NCPU")]
        public long NCPU { get; set; }

        [JsonPropertyName("MemTotal")]
        public long MemTotal { get; set; }

        [JsonPropertyName("GenericResources")]
        public IList<GenericResource> GenericResources { get; set; }

        [JsonPropertyName("DockerRootDir")]
        public string DockerRootDir { get; set; }

        [JsonPropertyName("HttpProxy")]
        public string HTTPProxy { get; set; }

        [JsonPropertyName("HttpsProxy")]
        public string HTTPSProxy { get; set; }

        [JsonPropertyName("NoProxy")]
        public string NoProxy { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Labels")]
        public IList<string> Labels { get; set; }

        [JsonPropertyName("ExperimentalBuild")]
        public bool ExperimentalBuild { get; set; }

        [JsonPropertyName("ServerVersion")]
        public string ServerVersion { get; set; }

        [JsonPropertyName("Runtimes")]
        public IDictionary<string, Runtime> Runtimes { get; set; }

        [JsonPropertyName("DefaultRuntime")]
        public string DefaultRuntime { get; set; }

        [JsonPropertyName("Swarm")]
        public Info Swarm { get; set; }

        [JsonPropertyName("LiveRestoreEnabled")]
        public bool LiveRestoreEnabled { get; set; }

        [JsonPropertyName("Isolation")]
        public string Isolation { get; set; }

        [JsonPropertyName("InitBinary")]
        public string InitBinary { get; set; }

        [JsonPropertyName("ContainerdCommit")]
        public Commit ContainerdCommit { get; set; }

        [JsonPropertyName("RuncCommit")]
        public Commit RuncCommit { get; set; }

        [JsonPropertyName("InitCommit")]
        public Commit InitCommit { get; set; }

        [JsonPropertyName("SecurityOptions")]
        public IList<string> SecurityOptions { get; set; }

        [JsonPropertyName("ProductLicense")]
        public string ProductLicense { get; set; }

        [JsonPropertyName("DefaultAddressPools")]
        public IList<NetworkAddressPool> DefaultAddressPools { get; set; }

        [JsonPropertyName("Warnings")]
        public IList<string> Warnings { get; set; }
    }
}
