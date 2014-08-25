using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{

	[DataContract]
	public class ContainerState
	{
		[DataMember (Name = "StartedAt")]
		public string StartedAt { get; set; }

		[DataMember (Name = "FinishedAt")]
		public string FinishedAt { get; set; }

		[DataMember (Name = "Running")]
		public bool? Running { get; set; }

		[DataMember (Name = "Paused")]
		public bool? Paused { get; set; }

		[DataMember (Name = "Pid")]
		public int? Pid { get; set; }

		[DataMember (Name = "ExitCode")]
		public int? ExitCode { get; set; }

		public ContainerState ()
		{
		}
	}
}

