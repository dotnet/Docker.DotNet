using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class NetworksPruneResponse // (types.NetworksPruneReport)
    {
        [JsonPropertyName("NetworksDeleted")]
        public IList<string> NetworksDeleted { get; set; }
    }
}
