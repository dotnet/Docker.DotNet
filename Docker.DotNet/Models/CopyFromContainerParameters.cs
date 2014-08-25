using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class CopyFromContainerParameters
	{
		[DataMember (Name = "Resource")]
		public string Resource { get; set; }

		public CopyFromContainerParameters ()
		{
		}
	}
}
