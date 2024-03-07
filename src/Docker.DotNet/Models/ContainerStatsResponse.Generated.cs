using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerStatsResponse // (types.StatsJSON)
    {
        public ContainerStatsResponse()
        {
        }

        public ContainerStatsResponse(Stats Stats)
        {
            if (Stats != null)
            {
                this.Read = Stats.Read;
                this.PreRead = Stats.PreRead;
                this.PidsStats = Stats.PidsStats;
                this.BlkioStats = Stats.BlkioStats;
                this.NumProcs = Stats.NumProcs;
                this.StorageStats = Stats.StorageStats;
                this.CPUStats = Stats.CPUStats;
                this.PreCPUStats = Stats.PreCPUStats;
                this.MemoryStats = Stats.MemoryStats;
            }
        }

        [JsonPropertyName("read")]
        public DateTime Read { get; set; }

        [JsonPropertyName("preread")]
        public DateTime PreRead { get; set; }

        [JsonPropertyName("pids_stats")]
        public PidsStats PidsStats { get; set; }

        [JsonPropertyName("blkio_stats")]
        public BlkioStats BlkioStats { get; set; }

        [JsonPropertyName("num_procs")]
        public uint NumProcs { get; set; }

        [JsonPropertyName("storage_stats")]
        public StorageStats StorageStats { get; set; }

        [JsonPropertyName("cpu_stats")]
        public CPUStats CPUStats { get; set; }

        [JsonPropertyName("precpu_stats")]
        public CPUStats PreCPUStats { get; set; }

        [JsonPropertyName("memory_stats")]
        public MemoryStats MemoryStats { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("networks")]
        public IDictionary<string, NetworkStats> Networks { get; set; }
    }
}
