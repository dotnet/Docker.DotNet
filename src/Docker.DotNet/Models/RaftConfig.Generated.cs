using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class RaftConfig // (swarm.RaftConfig)
    {
        [JsonPropertyName("SnapshotInterval")]
        public ulong SnapshotInterval { get; set; }

        [JsonPropertyName("KeepOldSnapshots")]
        public ulong? KeepOldSnapshots { get; set; }

        [JsonPropertyName("LogEntriesForSlowFollowers")]
        public ulong LogEntriesForSlowFollowers { get; set; }

        [JsonPropertyName("ElectionTick")]
        public long ElectionTick { get; set; }

        [JsonPropertyName("HeartbeatTick")]
        public long HeartbeatTick { get; set; }
    }
}
