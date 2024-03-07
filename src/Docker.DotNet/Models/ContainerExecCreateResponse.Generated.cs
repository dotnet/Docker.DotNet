using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerExecCreateResponse // (main.ContainerExecCreateResponse)
    {
        [JsonPropertyName("Id")]
        public string ID { get; set; }
    }
}
