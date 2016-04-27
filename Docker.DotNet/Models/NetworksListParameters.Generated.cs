using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworksListParameters // (types.NetworkListOptions)
    {
        [QueryStringParameter("filters", false)]
        public Args Filters { get; set; }
    }
}
