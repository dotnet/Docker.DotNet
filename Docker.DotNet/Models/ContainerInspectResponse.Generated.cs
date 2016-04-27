using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerInspectResponse // (types.ContainerJSON)
    {
        public ContainerInspectResponse()
        {
        }

        public ContainerInspectResponse(ContainerJSONBase ContainerJSONBase)
        {
            if (ContainerJSONBase != null)
            {
                this.ID = ContainerJSONBase.ID;
                this.Created = ContainerJSONBase.Created;
                this.Path = ContainerJSONBase.Path;
                this.Args = ContainerJSONBase.Args;
                this.State = ContainerJSONBase.State;
                this.Image = ContainerJSONBase.Image;
                this.ResolvConfPath = ContainerJSONBase.ResolvConfPath;
                this.HostnamePath = ContainerJSONBase.HostnamePath;
                this.HostsPath = ContainerJSONBase.HostsPath;
                this.LogPath = ContainerJSONBase.LogPath;
                this.Node = ContainerJSONBase.Node;
                this.Name = ContainerJSONBase.Name;
                this.RestartCount = ContainerJSONBase.RestartCount;
                this.Driver = ContainerJSONBase.Driver;
                this.MountLabel = ContainerJSONBase.MountLabel;
                this.ProcessLabel = ContainerJSONBase.ProcessLabel;
                this.AppArmorProfile = ContainerJSONBase.AppArmorProfile;
                this.ExecIDs = ContainerJSONBase.ExecIDs;
                this.HostConfig = ContainerJSONBase.HostConfig;
                this.GraphDriver = ContainerJSONBase.GraphDriver;
                this.SizeRw = ContainerJSONBase.SizeRw;
                this.SizeRootFs = ContainerJSONBase.SizeRootFs;
            }
        }

        [DataMember(Name = "Id")]
        public string ID { get; set; }

        [DataMember(Name = "Created")]
        public string Created { get; set; }

        [DataMember(Name = "Path")]
        public string Path { get; set; }

        [DataMember(Name = "Args")]
        public IList<string> Args { get; set; }

        [DataMember(Name = "State")]
        public ContainerState State { get; set; }

        [DataMember(Name = "Image")]
        public string Image { get; set; }

        [DataMember(Name = "ResolvConfPath")]
        public string ResolvConfPath { get; set; }

        [DataMember(Name = "HostnamePath")]
        public string HostnamePath { get; set; }

        [DataMember(Name = "HostsPath")]
        public string HostsPath { get; set; }

        [DataMember(Name = "LogPath")]
        public string LogPath { get; set; }

        [DataMember(Name = "Node")]
        public ContainerNode Node { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "RestartCount")]
        public int RestartCount { get; set; }

        [DataMember(Name = "Driver")]
        public string Driver { get; set; }

        [DataMember(Name = "MountLabel")]
        public string MountLabel { get; set; }

        [DataMember(Name = "ProcessLabel")]
        public string ProcessLabel { get; set; }

        [DataMember(Name = "AppArmorProfile")]
        public string AppArmorProfile { get; set; }

        [DataMember(Name = "ExecIDs")]
        public IList<string> ExecIDs { get; set; }

        [DataMember(Name = "HostConfig")]
        public HostConfig HostConfig { get; set; }

        [DataMember(Name = "GraphDriver")]
        public GraphDriverData GraphDriver { get; set; }

        [DataMember(Name = "SizeRw")]
        public long SizeRw { get; set; }

        [DataMember(Name = "SizeRootFs")]
        public long SizeRootFs { get; set; }

        [DataMember(Name = "Mounts")]
        public IList<MountPoint> Mounts { get; set; }

        [DataMember(Name = "Config")]
        public Config Config { get; set; }

        [DataMember(Name = "NetworkSettings")]
        public NetworkSettings NetworkSettings { get; set; }
    }
}
