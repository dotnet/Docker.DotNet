using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class Config
	{
		[DataMember (Name = "Hostname")]
		public string Hostname { get; set; }

		[DataMember (Name = "Domainname")]
		public string DomainName { get; set; }

		[DataMember (Name = "Image")]
		public string Image { get; set; }

		[DataMember (Name = "User")]
		public string User { get; set; }

		[DataMember (Name = "Memory")]
		public long Memory { get; set; }

		[DataMember (Name = "MemorySwap")]
		public long MemorySwap { get; set; }

		[DataMember (Name = "CpuShares")]
		public long CpuShares { get; set; }

		[DataMember (Name = "CpuSet")]
		public string CpuSet { get; set; }

		[DataMember (Name = "AttachStdin")]
		public bool AttachStdin { get; set; }

		[DataMember (Name = "AttachStdout")]
		public bool AttachStdout { get; set; }

		[DataMember (Name = "AttachStderr")]
		public bool AttachStderr { get; set; }

		[DataMember (Name = "PortSpecs")]
		public IList<string> PortSpecs { get; set; }

		[DataMember (Name = "Tty")]
		public bool Tty { get; set; }

		[DataMember (Name = "OpenStdin")]
		public bool OpenStdin { get; set; }

		[DataMember (Name = "StdinOnce")]
		public bool StdinOnce { get; set; }

		[DataMember (Name = "Env")]
		public IList<string> Env { get; set; }

		[DataMember (Name = "Cmd")]
		public IList<string> Cmd { get; set; }

		[DataMember (Name = "Entrypoint")]
		public IList<string> Entrypoint { get; set; }

		[DataMember (Name = "OnBuild")]
		public IList<string> OnBuild { get; set; }

		[DataMember (Name = "Dns")]
		public IList<string> Dns { get; set; }

		[DataMember (Name = "WorkingDir")]
		public string WorkingDir { get; set; }

		[DataMember (Name = "NetworkDisabled")]
		public bool NetworkDisabled { get; set; }

		[DataMember (Name = "VolumesFrom")]
		public string VolumesFrom { get; set; }

		[DataMember (Name = "Volumes")]
		public IDictionary<string, object> Volumes { get; set; }

		[DataMember (Name = "ExposedPorts")]
		public IDictionary<string, object> ExposedPorts { get; set; }

		public Config ()
		{
		}
	}
}

