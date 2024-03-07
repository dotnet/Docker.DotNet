using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class SwarmUpdateParameters // (main.SwarmUpdateParameters)
    {
        [JsonPropertyName("Spec")]
        public Spec Spec { get; set; }

        [QueryStringParameter("version", true)]
        public long Version { get; set; }

        [QueryStringParameter("rotateworkertoken", false, typeof(BoolQueryStringConverter))]
        public bool? RotateWorkerToken { get; set; }

        [QueryStringParameter("rotatemanagertoken", false, typeof(BoolQueryStringConverter))]
        public bool? RotateManagerToken { get; set; }

        [QueryStringParameter("rotatemanagerunlockkey", false, typeof(BoolQueryStringConverter))]
        public bool? RotateManagerUnlockKey { get; set; }
    }
}
