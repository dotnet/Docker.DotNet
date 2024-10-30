using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class ContainerExecStartParameters // (main.ContainerExecStartParameters)
    {
        [JsonPropertyName("User")]
        public string User { get; set; }

        [JsonPropertyName("Privileged")]
        public bool Privileged { get; set; }

        [JsonPropertyName("Tty")]
        public bool Tty { get; set; }

        [JsonPropertyName("ConsoleSize")]
        public ulong[] ConsoleSize { get; set; }

        [JsonPropertyName("AttachStdin")]
        public bool AttachStdin { get; set; }

        [JsonPropertyName("AttachStderr")]
        public bool AttachStderr { get; set; }

        [JsonPropertyName("AttachStdout")]
        public bool AttachStdout { get; set; }

        [JsonPropertyName("Detach")]
        public bool Detach { get; set; }

        [JsonPropertyName("DetachKeys")]
        public string DetachKeys { get; set; }

        [JsonPropertyName("Env")]
        public IList<string> Env { get; set; }

        [JsonPropertyName("WorkingDir")]
        public string WorkingDir { get; set; }

        [JsonPropertyName("Cmd")]
        public IList<string> Cmd { get; set; }
    }
}
