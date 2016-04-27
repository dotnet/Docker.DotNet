using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworkStats // (types.NetworkStats)
    {
        [DataMember(Name = "rx_bytes")]
        public ulong RxBytes { get; set; }

        [DataMember(Name = "rx_packets")]
        public ulong RxPackets { get; set; }

        [DataMember(Name = "rx_errors")]
        public ulong RxErrors { get; set; }

        [DataMember(Name = "rx_dropped")]
        public ulong RxDropped { get; set; }

        [DataMember(Name = "tx_bytes")]
        public ulong TxBytes { get; set; }

        [DataMember(Name = "tx_packets")]
        public ulong TxPackets { get; set; }

        [DataMember(Name = "tx_errors")]
        public ulong TxErrors { get; set; }

        [DataMember(Name = "tx_dropped")]
        public ulong TxDropped { get; set; }
    }
}
