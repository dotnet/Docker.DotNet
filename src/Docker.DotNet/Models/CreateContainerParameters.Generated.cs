using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Docker.DotNet.Models
{
    public class CreateContainerParameters // (main.CreateContainerParameters)
    {
        public CreateContainerParameters()
        {
        }

        public CreateContainerParameters(Config Config)
        {
            if (Config != null)
            {
                this.Hostname = Config.Hostname;
                this.Domainname = Config.Domainname;
                this.User = Config.User;
                this.AttachStdin = Config.AttachStdin;
                this.AttachStdout = Config.AttachStdout;
                this.AttachStderr = Config.AttachStderr;
                this.ExposedPorts = Config.ExposedPorts;
                this.Tty = Config.Tty;
                this.OpenStdin = Config.OpenStdin;
                this.StdinOnce = Config.StdinOnce;
                this.Env = Config.Env;
                this.Cmd = Config.Cmd;
                this.Healthcheck = Config.Healthcheck;
                this.ArgsEscaped = Config.ArgsEscaped;
                this.Image = Config.Image;
                this.Volumes = Config.Volumes;
                this.WorkingDir = Config.WorkingDir;
                this.Entrypoint = Config.Entrypoint;
                this.NetworkDisabled = Config.NetworkDisabled;
                this.MacAddress = Config.MacAddress;
                this.OnBuild = Config.OnBuild;
                this.Labels = Config.Labels;
                this.StopSignal = Config.StopSignal;
                this.StopTimeout = Config.StopTimeout;
                this.Shell = Config.Shell;
            }
        }

        [QueryStringParameter("name", false)]
        public string Name { get; set; }

        [QueryStringParameter("platform", false)]
        public string Platform { get; set; }

        [JsonPropertyName("Hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("Domainname")]
        public string Domainname { get; set; }

        [JsonPropertyName("User")]
        public string User { get; set; }

        [JsonPropertyName("AttachStdin")]
        public bool AttachStdin { get; set; }

        [JsonPropertyName("AttachStdout")]
        public bool AttachStdout { get; set; }

        [JsonPropertyName("AttachStderr")]
        public bool AttachStderr { get; set; }

        [JsonPropertyName("ExposedPorts")]
        public IDictionary<string, EmptyStruct> ExposedPorts { get; set; }

        [JsonPropertyName("Tty")]
        public bool Tty { get; set; }

        [JsonPropertyName("OpenStdin")]
        public bool OpenStdin { get; set; }

        [JsonPropertyName("StdinOnce")]
        public bool StdinOnce { get; set; }

        [JsonPropertyName("Env")]
        public IList<string> Env { get; set; }

        [JsonPropertyName("Cmd")]
        public IList<string> Cmd { get; set; }

        [JsonPropertyName("Healthcheck")]
        public HealthConfig Healthcheck { get; set; }

        [JsonPropertyName("ArgsEscaped")]
        public bool ArgsEscaped { get; set; }

        [JsonPropertyName("Image")]
        public string Image { get; set; }

        [JsonPropertyName("Volumes")]
        public IDictionary<string, EmptyStruct> Volumes { get; set; }

        [JsonPropertyName("WorkingDir")]
        public string WorkingDir { get; set; }

        [JsonPropertyName("Entrypoint")]
        public IList<string> Entrypoint { get; set; }

        [JsonPropertyName("NetworkDisabled")]
        public bool NetworkDisabled { get; set; }

        [JsonPropertyName("MacAddress")]
        public string MacAddress { get; set; }

        [JsonPropertyName("OnBuild")]
        public IList<string> OnBuild { get; set; }

        [JsonPropertyName("Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [JsonPropertyName("StopSignal")]
        public string StopSignal { get; set; }

        [JsonPropertyName("StopTimeout")]
        [JsonConverter(typeof(TimeSpanSecondsConverter))]
        public TimeSpan? StopTimeout { get; set; }

        [JsonPropertyName("Shell")]
        public IList<string> Shell { get; set; }

        [JsonPropertyName("HostConfig")]
        public HostConfig HostConfig { get; set; }

        [JsonPropertyName("NetworkingConfig")]
        public NetworkingConfig NetworkingConfig { get; set; }
    }
}
