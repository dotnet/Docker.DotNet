using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerResponse
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "Config")]
        public Config Config { get; set; }

        [DataMember(Name = "Parent")]
        public string Parent { get; set; }

        [DataMember(Name = "Path")]
        public string Path { get; set; }

        [DataMember(Name = "ProcessLabel")]
        public string ProcessLabel { get; set; }

        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "ExecDriver")]
        public string ExecDriver { get; set; }

        [DataMember(Name = "MountLabel")]
        public string MountLabel { get; set; }

        [DataMember(Name = "HostsPath")]
        public string HostsPath { get; set; }

        [DataMember(Name = "HostnamePath")]
        public string HostnamePath { get; set; }

        [DataMember(Name = "Args")]
        public IList<string> Args { get; set; }

        [DataMember(Name = "Image")]
        public string Image { get; set; }

        [DataMember(Name = "State")]
        public ContainerState State { get; set; }

        [DataMember(Name = "NetworkSettings")]
        public NetworkSettings NetworkSettings { get; set; }

        [DataMember(Name = "SysInitPath")]
        public string SysInitPath { get; set; }

        [DataMember(Name = "ResolvConfPath")]
        public string ResolvConfPath { get; set; }

        [DataMember(Name = "HostConfig")]
        public HostConfig HostConfig { get; set; }

        [DataMember(Name = "Volumes")]
        public IDictionary<string, string> Volumes { get; set; }

        [DataMember(Name = "VolumesRW")]
        public IDictionary<string, bool> VolumesRW { get; set; }

        public ContainerResponse()
        {
        }
    }
}