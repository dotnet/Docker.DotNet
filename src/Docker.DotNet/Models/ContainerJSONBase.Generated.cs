using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerJSONBase // (types.ContainerJSONBase)
    {
        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("Created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("Path")]
        public string Path { get; set; }

        [JsonPropertyName("Args")]
        public IList<string> Args { get; set; }

        [JsonPropertyName("State")]
        public ContainerState State { get; set; }

        [JsonPropertyName("Image")]
        public string Image { get; set; }

        [JsonPropertyName("ResolvConfPath")]
        public string ResolvConfPath { get; set; }

        [JsonPropertyName("HostnamePath")]
        public string HostnamePath { get; set; }

        [JsonPropertyName("HostsPath")]
        public string HostsPath { get; set; }

        [JsonPropertyName("LogPath")]
        public string LogPath { get; set; }

        [JsonPropertyName("Node")]
        public ContainerNode Node { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("RestartCount")]
        public long RestartCount { get; set; }

        [JsonPropertyName("Driver")]
        public string Driver { get; set; }

        [JsonPropertyName("Platform")]
        public string Platform { get; set; }

        [JsonPropertyName("MountLabel")]
        public string MountLabel { get; set; }

        [JsonPropertyName("ProcessLabel")]
        public string ProcessLabel { get; set; }

        [JsonPropertyName("AppArmorProfile")]
        public string AppArmorProfile { get; set; }

        [JsonPropertyName("ExecIDs")]
        public IList<string> ExecIDs { get; set; }

        [JsonPropertyName("HostConfig")]
        public HostConfig HostConfig { get; set; }

        [JsonPropertyName("GraphDriver")]
        public GraphDriverData GraphDriver { get; set; }

        [JsonPropertyName("SizeRw")]
        public long? SizeRw { get; set; }

        [JsonPropertyName("SizeRootFs")]
        public long? SizeRootFs { get; set; }
    }
}
