
namespace Docker.DotNet.Models
{
    public class PluginCreateParameters // (main.PluginCreateParameters)
    {
        [QueryStringParameter("name", true)]
        public string Name { get; set; }
    }
}
