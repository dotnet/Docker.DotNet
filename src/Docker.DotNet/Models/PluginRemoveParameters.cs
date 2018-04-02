using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class PluginRemoveParameters // (main.ContainerRemoveParameters)
    {
        [QueryStringParameter("force", false, typeof(BoolQueryStringConverter))]
        public bool? Force { get; set; }
    }
}
