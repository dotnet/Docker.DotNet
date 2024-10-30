using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Message // (events.Message)
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Action")]
        public string Action { get; set; }

        [JsonPropertyName("Actor")]
        public Actor Actor { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("timeNano")]
        public long TimeNano { get; set; }
    }
}
