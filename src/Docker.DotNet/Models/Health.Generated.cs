using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class Health // (types.Health)
    {
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("FailingStreak")]
        public long FailingStreak { get; set; }

        [JsonPropertyName("Log")]
        public IList<HealthcheckResult> Log { get; set; }
    }
}
