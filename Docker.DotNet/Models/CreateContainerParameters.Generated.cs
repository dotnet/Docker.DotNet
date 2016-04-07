using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
    [DataContract]
    public class CreateContainerParameters // (main.ContainerCreateOptions)
    {
        public CreateContainerParameters()
        {
        }

        public CreateContainerParameters(Config Config)
        {
            if (Config != null)            {
                this.Hostname = Config.Hostname;
                this.Domainname = Config.Domainname;
                this.User = Config.User;
                this.AttachStdin = Config.AttachStdin;
                this.AttachStdout = Config.AttachStdout;
                this.AttachStderr = Config.AttachStderr;
                this.ExposedPorts = Config.ExposedPorts;
                this.PublishService = Config.PublishService;
                this.Tty = Config.Tty;
                this.OpenStdin = Config.OpenStdin;
                this.StdinOnce = Config.StdinOnce;
                this.Env = Config.Env;
                this.Cmd = Config.Cmd;
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
            }
        }

        [QueryStringParameter("name", false)]
        public string Name { get; set; }

        [DataMember(Name = "Hostname")]
        public string Hostname { get; set; }

        [DataMember(Name = "Domainname")]
        public string Domainname { get; set; }

        [DataMember(Name = "User")]
        public string User { get; set; }

        [DataMember(Name = "AttachStdin")]
        public bool AttachStdin { get; set; }

        [DataMember(Name = "AttachStdout")]
        public bool AttachStdout { get; set; }

        [DataMember(Name = "AttachStderr")]
        public bool AttachStderr { get; set; }

        [DataMember(Name = "ExposedPorts")]
        public ISet<string> ExposedPorts { get; set; }

        [DataMember(Name = "PublishService")]
        public string PublishService { get; set; }

        [DataMember(Name = "Tty")]
        public bool Tty { get; set; }

        [DataMember(Name = "OpenStdin")]
        public bool OpenStdin { get; set; }

        [DataMember(Name = "StdinOnce")]
        public bool StdinOnce { get; set; }

        [DataMember(Name = "Env")]
        public IList<string> Env { get; set; }

        [DataMember(Name = "Cmd")]
        public IList<string> Cmd { get; set; }

        [DataMember(Name = "ArgsEscaped")]
        public bool ArgsEscaped { get; set; }

        [DataMember(Name = "Image")]
        public string Image { get; set; }

        [DataMember(Name = "Volumes")]
        public ISet<string> Volumes { get; set; }

        [DataMember(Name = "WorkingDir")]
        public string WorkingDir { get; set; }

        [DataMember(Name = "Entrypoint")]
        public IList<string> Entrypoint { get; set; }

        [DataMember(Name = "NetworkDisabled")]
        public bool NetworkDisabled { get; set; }

        [DataMember(Name = "MacAddress")]
        public string MacAddress { get; set; }

        [DataMember(Name = "OnBuild")]
        public IList<string> OnBuild { get; set; }

        [DataMember(Name = "Labels")]
        public IDictionary<string, string> Labels { get; set; }

        [DataMember(Name = "StopSignal")]
        public string StopSignal { get; set; }

        [DataMember(Name = "HostConfig")]
        public HostConfig HostConfig { get; set; }

        [DataMember(Name = "NetworkingConfig")]
        public NetworkingConfig NetworkingConfig { get; set; }
    }
}
