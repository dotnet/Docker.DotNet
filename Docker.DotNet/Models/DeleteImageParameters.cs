namespace Docker.DotNet.Models
{
    public class DeleteImageParameters
    {
        [QueryStringParameter("force", false, typeof (BoolQueryStringConverter))]
        public bool? Force { get; set; }

        [QueryStringParameter("noprune", false, typeof (BoolQueryStringConverter))]
        public bool? NoPrune { get; set; }

        public DeleteImageParameters()
        {
        }
    }
}