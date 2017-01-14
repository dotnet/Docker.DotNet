using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkStats // (types.NetworkStats)
    {
        [DataMember(Name = "rx_bytes", EmitDefaultValue = false)]
        public ulong RxBytes { get; set; }

        [DataMember(Name = "rx_packets", EmitDefaultValue = false)]
        public ulong RxPackets { get; set; }

        [DataMember(Name = "rx_errors", EmitDefaultValue = false)]
        public ulong RxErrors { get; set; }

        [DataMember(Name = "rx_dropped", EmitDefaultValue = false)]
        public ulong RxDropped { get; set; }

        [DataMember(Name = "tx_bytes", EmitDefaultValue = false)]
        public ulong TxBytes { get; set; }

        [DataMember(Name = "tx_packets", EmitDefaultValue = false)]
        public ulong TxPackets { get; set; }

        [DataMember(Name = "tx_errors", EmitDefaultValue = false)]
        public ulong TxErrors { get; set; }

        [DataMember(Name = "tx_dropped", EmitDefaultValue = false)]
        public ulong TxDropped { get; set; }
    }
}
