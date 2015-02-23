namespace Docker.DotNet.Models
{
    public class ListProcessesParameters
    {
        [QueryStringParameter("ps_args", false)]
        public string PsArgs { get; set; }

        public ListProcessesParameters()
        {
        }
    }
}