namespace Docker.DotNet.Models
{
    public class SearchImagesParameters
    {
        [QueryStringParameter("term", true)]
        public string Term { get; set; }

        public SearchImagesParameters()
        {
        }
    }
}