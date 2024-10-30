using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class WaitExitError // (container.WaitExitError)
    {
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
