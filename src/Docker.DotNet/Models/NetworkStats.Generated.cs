using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworkStats // (types.NetworkStats)
    {
        [JsonPropertyName("rx_bytes")]
        public ulong RxBytes { get; set; }

        [JsonPropertyName("rx_packets")]
        public ulong RxPackets { get; set; }

        [JsonPropertyName("rx_errors")]
        public ulong RxErrors { get; set; }

        [JsonPropertyName("rx_dropped")]
        public ulong RxDropped { get; set; }

        [JsonPropertyName("tx_bytes")]
        public ulong TxBytes { get; set; }

        [JsonPropertyName("tx_packets")]
        public ulong TxPackets { get; set; }

        [JsonPropertyName("tx_errors")]
        public ulong TxErrors { get; set; }

        [JsonPropertyName("tx_dropped")]
        public ulong TxDropped { get; set; }

        [JsonPropertyName("endpoint_id")]
        public string EndpointID { get; set; }

        [JsonPropertyName("instance_id")]
        public string InstanceID { get; set; }
    }
}
