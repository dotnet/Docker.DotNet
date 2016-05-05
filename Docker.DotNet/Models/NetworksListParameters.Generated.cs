using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class NetworksListParameters // (main.NetworkListParameters)
    {
        [QueryStringParameter("filters", false)]
        public Args Filters { get; set; }
    }
}
