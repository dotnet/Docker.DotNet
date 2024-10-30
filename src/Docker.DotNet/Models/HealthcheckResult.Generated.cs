using System;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class HealthcheckResult // (types.HealthcheckResult)
    {
        [JsonPropertyName("Start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("End")]
        public DateTime End { get; set; }

        [JsonPropertyName("ExitCode")]
        public long ExitCode { get; set; }

        [JsonPropertyName("Output")]
        public string Output { get; set; }
    }
}
