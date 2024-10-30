using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ServiceUpdateResponse // (types.ServiceUpdateResponse)
    {
        [JsonPropertyName("Warnings")]
        public IList<string> Warnings { get; set; }
    }
}
