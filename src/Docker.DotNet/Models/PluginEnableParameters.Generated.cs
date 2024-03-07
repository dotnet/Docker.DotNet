
namespace Docker.DotNet.Models
{
    public class PluginEnableParameters // (main.PluginEnableParameters)
    {
        [QueryStringParameter("timeout", false)]
        public long? Timeout { get; set; }
    }
}
