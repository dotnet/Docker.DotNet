using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkAttachmentSpec // (swarm.NetworkAttachmentSpec)
    {
        [JsonPropertyName("ContainerID")]
        public string ContainerID { get; set; }
    }
}
