using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImageHistoryResponse // (image.HistoryResponseItem)
    {
        [JsonPropertyName("Comment")]
        public string Comment { get; set; }

        [JsonPropertyName("Created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("Id")]
        public string ID { get; set; }

        [JsonPropertyName("Size")]
        public long Size { get; set; }

        [JsonPropertyName("Tags")]
        public IList<string> Tags { get; set; }
    }
}
