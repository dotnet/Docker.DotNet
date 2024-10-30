using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class JSONMessage // (jsonmessage.JSONMessage)
    {
        [JsonPropertyName("stream")]
        public string Stream { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("progressDetail")]
        public JSONProgress Progress { get; set; }

        [JsonPropertyName("progress")]
        public string ProgressMessage { get; set; }

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("timeNano")]
        public long TimeNano { get; set; }

        [JsonPropertyName("errorDetail")]
        public JSONError Error { get; set; }

        [JsonPropertyName("error")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("aux")]
        public ObjectExtensionData Aux { get; set; }
    }
}
