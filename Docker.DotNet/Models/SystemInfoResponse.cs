using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class SystemInfoResponse
    {
        [DataMember(Name = "Containers")]
        public long? Containers { get; set; }

        [DataMember(Name = "Images")]
        public long? Images { get; set; }

        [DataMember(Name = "MemoryLimit")]
        public bool? MemoryLimit { get; set; }

        [DataMember(Name = "SwapLimit")]
        public bool? SwapLimit { get; set; }

        [DataMember(Name = "IPv4Forwarding")]
        public bool? IPv4Forwarding { get; set; }

        [DataMember(Name = "NFd")]
        public long? NFd { get; set; }

        [DataMember(Name = "NGoroutines")]
        public long? NGoroutines { get; set; }

        [DataMember(Name = "NEventsListener")]
        public long? NEventsListener { get; set; }

        [DataMember(Name = "Sockets")]
        public IList<string> Sockets { get; set; }

        [DataMember(Name = "KernelVersion")]
        public string KernelVersion { get; set; }

        [DataMember(Name = "InitPath")]
        public string InitPath { get; set; }

        [DataMember(Name = "InitSha1")]
        public string InitSha1 { get; set; }

        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "DriverStatus")]
        public IList<IList<string>> DriverStatus { get; set; }

        [DataMember(Name = "ExecutionDriver")]
        public string ExecutionDriver { get; set; }

        [DataMember(Name = "IndexServerAddress")]
        public string IndexServerAddress { get; set; }

        [DataMember(Name = "Debug")]
        public bool? Debug { get; set; }

        [DataMember(Name = "NCPU")]
        public string NCPU { get;set; }

        [DataMember(Name = "MemTotal")]
        public string MemTotal { get;set; }

        [DataMember(Name = "ID")]
        public string ID { get;set; }

        [DataMember(Name = "DockerRootDir")]
        public string DockerRootDir { get;set; }

        [DataMember(Name = "OperatingSystem")]
        public string OperatingSystem { get;set; }

        [DataMember(Name = "Labels")]
        public IList<string> Labels { get;set; }

        public SystemInfoResponse()
        {
        }
    }
}