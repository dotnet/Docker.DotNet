using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ImagesCreateParameters // (main.ImagesCreateParameters)
    {
        [QueryStringParameter("fromImage", false)]
        public string FromImage { get; set; }

        [QueryStringParameter("fromSrc", false)]
        public string FromSrc { get; set; }

        [QueryStringParameter("repo", false)]
        public string Repo { get; set; }

        [QueryStringParameter("tag", false)]
        public string Tag { get; set; }

        [QueryStringParameter("message", false)]
        public string Message { get; set; }

        [QueryStringParameter("changes", false, typeof(EnumerableQueryStringConverter))]
        public IList<string> Changes { get; set; }

        [QueryStringParameter("platform", false)]
        public string Platform { get; set; }

        [JsonPropertyName("RegistryAuth")]
        public AuthConfig RegistryAuth { get; set; }
    }
}
