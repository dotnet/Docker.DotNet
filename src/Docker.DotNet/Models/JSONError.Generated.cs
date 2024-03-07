using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class JSONError // (jsonmessage.JSONError)
    {
        [JsonPropertyName("code")]
        public long Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
