using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class CommitContainerChangesResponse // (main.CommitContainerChangesResponse)
    {
        [JsonPropertyName("Id")]
        public string ID { get; set; }
    }
}
