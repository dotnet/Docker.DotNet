using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerSpec // (swarm.ContainerSpec)
    {
        [JsonPropertyName("Image")]
        public string Image { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("Command")]
        public IList<string> Command { get; set; }

        [JsonPropertyName("Args")]
        public IList<string> Args { get; set; }

        [JsonPropertyName("Hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("Env")]
        public IList<string> Env { get; set; }

        [JsonPropertyName("Dir")]
        public string Dir { get; set; }

        [JsonPropertyName("User")]
        public string User { get; set; }

        [JsonPropertyName("Groups")]
        public IList<string> Groups { get; set; }

        [JsonPropertyName("Privileges")]
        public Privileges Privileges { get; set; }

        [JsonPropertyName("Init")]
        public bool? Init { get; set; }

        [JsonPropertyName("StopSignal")]
        public string StopSignal { get; set; }

        [JsonPropertyName("TTY")]
        public bool TTY { get; set; }

        [JsonPropertyName("OpenStdin")]
        public bool OpenStdin { get; set; }

        [JsonPropertyName("ReadOnly")]
        public bool ReadOnly { get; set; }

        [JsonPropertyName("Mounts")]
        public IList<Mount> Mounts { get; set; }

        [JsonPropertyName("StopGracePeriod")]
        public long? StopGracePeriod { get; set; }

        [JsonPropertyName("Healthcheck")]
        public HealthConfig Healthcheck { get; set; }

        [JsonPropertyName("Hosts")]
        public IList<string> Hosts { get; set; }

        [JsonPropertyName("DNSConfig")]
        public DNSConfig DNSConfig { get; set; }

        [JsonPropertyName("Secrets")]
        public IList<SecretReference> Secrets { get; set; }

        [JsonPropertyName("Configs")]
        public IList<SwarmConfigReference> Configs { get; set; }

        [JsonPropertyName("Isolation")]
        public string Isolation { get; set; }

        [JsonPropertyName("Sysctls")]
        public IDictionary<string, string> Sysctls { get; set; }

        [JsonPropertyName("CapabilityAdd")]
        public IList<string> CapabilityAdd { get; set; }

        [JsonPropertyName("CapabilityDrop")]
        public IList<string> CapabilityDrop { get; set; }

        [JsonPropertyName("Ulimits")]
        public IList<Ulimit> Ulimits { get; set; }
    }
}
