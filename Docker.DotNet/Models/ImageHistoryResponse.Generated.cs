using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ImageHistoryResponse // (types.ImageHistory)
    {
        [DataMember(Name = "Id")]
        public string ID { get; set; }

        [DataMember(Name = "Created")]
        public long Created { get; set; }

        [DataMember(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [DataMember(Name = "Tags")]
        public IList<string> Tags { get; set; }

        [DataMember(Name = "Size")]
        public long Size { get; set; }

        [DataMember(Name = "Comment")]
        public string Comment { get; set; }
    }
}
