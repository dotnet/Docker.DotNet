using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ObjectExtensionData
    {
        [JsonExtensionData]
        public IDictionary<string, object> ExtensionData { get; set; }
    }
}