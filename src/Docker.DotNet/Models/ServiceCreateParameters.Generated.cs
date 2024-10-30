using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ServiceCreateParameters // (main.ServiceCreateParameters)
    {
        [JsonPropertyName("Service")]
        public ServiceSpec Service { get; set; }

        [JsonPropertyName("RegistryAuth")]
        public AuthConfig RegistryAuth { get; set; }
    }
}
