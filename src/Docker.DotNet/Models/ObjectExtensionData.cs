using System.Collections.Generic;
using Newtonsoft.Json;

namespace Docker.DotNet.Models
{
    public class ObjectExtensionData
    {
        [JsonExtensionData]
        public IDictionary<string, object> ExtensionData { get; set; }
    }
}