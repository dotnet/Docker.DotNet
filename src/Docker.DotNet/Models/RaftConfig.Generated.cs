using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class RaftConfig // (swarm.RaftConfig)
    {
        [DataMember(Name = "SnapshotInterval", EmitDefaultValue = false)]
        public ulong SnapshotInterval { get; set; }

        [DataMember(Name = "KeepOldSnapshots", EmitDefaultValue = false)]
        public ulong KeepOldSnapshots { get; set; }

        [DataMember(Name = "LogEntriesForSlowFollowers", EmitDefaultValue = false)]
        public ulong LogEntriesForSlowFollowers { get; set; }

        [DataMember(Name = "HeartbeatTick", EmitDefaultValue = false)]
        public uint HeartbeatTick { get; set; }

        [DataMember(Name = "ElectionTick", EmitDefaultValue = false)]
        public uint ElectionTick { get; set; }
    }
}
