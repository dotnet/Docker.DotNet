using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class CAConfig // (swarm.CAConfig)
    {
        [JsonPropertyName("NodeCertExpiry")]
        public long NodeCertExpiry { get; set; }

        [JsonPropertyName("ExternalCAs")]
        public IList<ExternalCA> ExternalCAs { get; set; }

        [JsonPropertyName("SigningCACert")]
        public string SigningCACert { get; set; }

        [JsonPropertyName("SigningCAKey")]
        public string SigningCAKey { get; set; }

        [JsonPropertyName("ForceRotate")]
        public ulong ForceRotate { get; set; }
    }
}
