using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class ContainerStatsParameters // (main.ContainerStatsParameters)
    {
        [QueryStringParameter("stream", false, typeof(BoolQueryStringConverter))]
        public bool? Stream { get; set; }
    }
}
