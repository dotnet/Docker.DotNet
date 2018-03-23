using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PluginEnableParameters
    {
        [QueryStringParameter("timeout", false)]
        public string Timeout { get; set; }
    }
}