using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class BlkioStats // (types.BlkioStats)
    {
        [DataMember(Name = "io_service_bytes_recursive")]
        public IList<BlkioStatEntry> IoServiceBytesRecursive { get; set; }

        [DataMember(Name = "io_serviced_recursive")]
        public IList<BlkioStatEntry> IoServicedRecursive { get; set; }

        [DataMember(Name = "io_queue_recursive")]
        public IList<BlkioStatEntry> IoQueuedRecursive { get; set; }

        [DataMember(Name = "io_service_time_recursive")]
        public IList<BlkioStatEntry> IoServiceTimeRecursive { get; set; }

        [DataMember(Name = "io_wait_time_recursive")]
        public IList<BlkioStatEntry> IoWaitTimeRecursive { get; set; }

        [DataMember(Name = "io_merged_recursive")]
        public IList<BlkioStatEntry> IoMergedRecursive { get; set; }

        [DataMember(Name = "io_time_recursive")]
        public IList<BlkioStatEntry> IoTimeRecursive { get; set; }

        [DataMember(Name = "sectors_recursive")]
        public IList<BlkioStatEntry> SectorsRecursive { get; set; }
    }
}
