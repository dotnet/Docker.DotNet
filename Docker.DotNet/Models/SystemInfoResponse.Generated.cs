using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SystemInfoResponse // (types.Info)
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "Containers")]
        public int Containers { get; set; }

        [DataMember(Name = "ContainersRunning")]
        public int ContainersRunning { get; set; }

        [DataMember(Name = "ContainersPaused")]
        public int ContainersPaused { get; set; }

        [DataMember(Name = "ContainersStopped")]
        public int ContainersStopped { get; set; }

        [DataMember(Name = "Images")]
        public int Images { get; set; }

        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "DriverStatus")]
        public IList<string[]> DriverStatus { get; set; }

        [DataMember(Name = "SystemStatus")]
        public IList<string[]> SystemStatus { get; set; }

        [DataMember(Name = "Plugins")]
        public PluginsInfo Plugins { get; set; }

        [DataMember(Name = "MemoryLimit")]
        public bool MemoryLimit { get; set; }

        [DataMember(Name = "SwapLimit")]
        public bool SwapLimit { get; set; }

        [DataMember(Name = "KernelMemory")]
        public bool KernelMemory { get; set; }

        [DataMember(Name = "CpuCfsPeriod")]
        public bool CPUCfsPeriod { get; set; }

        [DataMember(Name = "CpuCfsQuota")]
        public bool CPUCfsQuota { get; set; }

        [DataMember(Name = "CPUShares")]
        public bool CPUShares { get; set; }

        [DataMember(Name = "CPUSet")]
        public bool CPUSet { get; set; }

        [DataMember(Name = "IPv4Forwarding")]
        public bool IPv4Forwarding { get; set; }

        [DataMember(Name = "BridgeNfIptables")]
        public bool BridgeNfIptables { get; set; }

        [DataMember(Name = "BridgeNfIp6tables")]
        public bool BridgeNfIP6tables { get; set; }

        [DataMember(Name = "Debug")]
        public bool Debug { get; set; }

        [DataMember(Name = "NFd")]
        public int NFd { get; set; }

        [DataMember(Name = "OomKillDisable")]
        public bool OomKillDisable { get; set; }

        [DataMember(Name = "NGoroutines")]
        public int NGoroutines { get; set; }

        [DataMember(Name = "SystemTime")]
        public string SystemTime { get; set; }

        [DataMember(Name = "ExecutionDriver")]
        public string ExecutionDriver { get; set; }

        [DataMember(Name = "LoggingDriver")]
        public string LoggingDriver { get; set; }

        [DataMember(Name = "CgroupDriver")]
        public string CgroupDriver { get; set; }

        [DataMember(Name = "NEventsListener")]
        public int NEventsListener { get; set; }

        [DataMember(Name = "KernelVersion")]
        public string KernelVersion { get; set; }

        [DataMember(Name = "OperatingSystem")]
        public string OperatingSystem { get; set; }

        [DataMember(Name = "OSType")]
        public string OSType { get; set; }

        [DataMember(Name = "Architecture")]
        public string Architecture { get; set; }

        [DataMember(Name = "IndexServerAddress")]
        public string IndexServerAddress { get; set; }

        [DataMember(Name = "RegistryConfig")]
        public ServiceConfig RegistryConfig { get; set; }

        [DataMember(Name = "NCPU")]
        public int NCPU { get; set; }

        [DataMember(Name = "MemTotal")]
        public long MemTotal { get; set; }

        [DataMember(Name = "DockerRootDir")]
        public string DockerRootDir { get; set; }

        [DataMember(Name = "HttpProxy")]
        public string HTTPProxy { get; set; }

        [DataMember(Name = "HttpsProxy")]
        public string HTTPSProxy { get; set; }

        [DataMember(Name = "NoProxy")]
        public string NoProxy { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Labels")]
        public IList<string> Labels { get; set; }

        [DataMember(Name = "ExperimentalBuild")]
        public bool ExperimentalBuild { get; set; }

        [DataMember(Name = "ServerVersion")]
        public string ServerVersion { get; set; }

        [DataMember(Name = "ClusterStore")]
        public string ClusterStore { get; set; }

        [DataMember(Name = "ClusterAdvertise")]
        public string ClusterAdvertise { get; set; }

        [DataMember(Name = "SecurityOptions")]
        public IList<string> SecurityOptions { get; set; }
    }
}
