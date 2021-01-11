using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class TLSInfo // (swarm.TLSInfo)
    {
        [DataMember(Name = "TrustRoot", EmitDefaultValue = false)]
        public string TrustRoot { get; set; }

        [DataMember(Name = "CertIssuerSubject", EmitDefaultValue = false)]
        public string CertIssuerSubject { get; set; }

        [DataMember(Name = "CertIssuerPublicKey", EmitDefaultValue = false)]
        public string CertIssuerPublicKey { get; set; }
    }
}
