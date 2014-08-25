using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class ContainerProcessesResponse
	{
		[DataMember (Name = "Titles")]
		public IList<string> Titles { get; set; }

		[DataMember (Name = "Processes")]
		public IList<IList<string>> Processes { get; set; }

		public ContainerProcessesResponse ()
		{
		}
	}
}

