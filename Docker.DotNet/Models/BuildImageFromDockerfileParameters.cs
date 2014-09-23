namespace Docker.DotNet.Models
{
    public class BuildImageFromDockerfileParameters
    {
        [QueryStringParameter("t", false)]
        public string RepositoryTagName { get; set; }

        [QueryStringParameter("q", false, typeof (BoolQueryStringConverter))]
        public bool? Quiet { get; set; }

        [QueryStringParameter("nocache", false, typeof (BoolQueryStringConverter))]
        public bool? NoCache { get; set; }

        [QueryStringParameter("rm", false, typeof (BoolQueryStringConverter))]
        public bool? RemoveIntermediateContainers { get; set; }

        [QueryStringParameter("forcerm", false, typeof (BoolQueryStringConverter))]
        public bool? ForceRemoveIntermediateContainers { get; set; }

        public BuildImageFromDockerfileParameters()
        {
        }
    }
}