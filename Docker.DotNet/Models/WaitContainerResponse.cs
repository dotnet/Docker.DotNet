using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class WaitContainerResponse
	{
		[DataMember (Name = "StatusCode")]
		public int StatusCode;

		public WaitContainerResponse ()
		{
		}
	}
}

