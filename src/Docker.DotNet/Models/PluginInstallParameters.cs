using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PluginInstallParameters
    {
        [QueryStringParameter("name", false)]
        public string Name { get; set; }

        [QueryStringParameter("remote", false)]
        public string Remote { get; set; }
    }
}
