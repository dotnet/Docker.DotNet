using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class TLSInfo // (swarm.TLSInfo)
    {
        [JsonPropertyName("TrustRoot")]
        public string TrustRoot { get; set; }

        [JsonPropertyName("CertIssuerSubject")]
        public IList<byte> CertIssuerSubject { get; set; }

        [JsonPropertyName("CertIssuerPublicKey")]
        public IList<byte> CertIssuerPublicKey { get; set; }
    }
}
