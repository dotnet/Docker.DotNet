using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class BlkioStats // (types.BlkioStats)
    {
        [JsonPropertyName("io_service_bytes_recursive")]
        public IList<BlkioStatEntry> IoServiceBytesRecursive { get; set; }

        [JsonPropertyName("io_serviced_recursive")]
        public IList<BlkioStatEntry> IoServicedRecursive { get; set; }

        [JsonPropertyName("io_queue_recursive")]
        public IList<BlkioStatEntry> IoQueuedRecursive { get; set; }

        [JsonPropertyName("io_service_time_recursive")]
        public IList<BlkioStatEntry> IoServiceTimeRecursive { get; set; }

        [JsonPropertyName("io_wait_time_recursive")]
        public IList<BlkioStatEntry> IoWaitTimeRecursive { get; set; }

        [JsonPropertyName("io_merged_recursive")]
        public IList<BlkioStatEntry> IoMergedRecursive { get; set; }

        [JsonPropertyName("io_time_recursive")]
        public IList<BlkioStatEntry> IoTimeRecursive { get; set; }

        [JsonPropertyName("sectors_recursive")]
        public IList<BlkioStatEntry> SectorsRecursive { get; set; }
    }
}
