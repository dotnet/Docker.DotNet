namespace Docker.DotNet.Models
{
    public class FilterServiceParameters
    {
        public string Id { get; set; }
        public string Label { get; set; }
        /// <summary> "replicated"|"global" </summary>
        public string Mode { get; set; }
        public string Name { get; set; }
    }
}