
namespace Docker.DotNet.Models
{
    public class PluginDisableParameters // (main.PluginDisableParameters)
    {
        [QueryStringParameter("force", false, typeof(BoolQueryStringConverter))]
        public bool? Force { get; set; }
    }
}
