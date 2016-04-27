using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class VolumesListParameters // (main.VolumesListParameters)
    {
        [QueryStringParameter("filters", false)]
        public Args Filters { get; set; }
    }
}
