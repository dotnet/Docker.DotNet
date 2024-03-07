using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerListResponse // (types.Container)
    {
        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("Names")]
        public IList<string> Names { get; set; }

        [JsonPropertyName("Image")]
        public string Image { get; set; }

        [JsonPropertyName("ImageID")]
        public string ImageID { get; set; }

        [JsonPropertyName("Command")]
        public string Command { get; set; }

        [JsonPropertyName("Created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("Ports")]
        public IList<Port> Ports { get; set; }

        [JsonPropertyName("SizeRw")]
        public long SizeRw { get; set; }

        [JsonPropertyName("SizeRootFs")]
        public long SizeRootFs { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("State")]
        public string State { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("NetworkSettings")]
        public SummaryNetworkSettings NetworkSettings { get; set; }

        [JsonPropertyName("Mounts")]
        public IList<MountPoint> Mounts { get; set; }
    }
}
