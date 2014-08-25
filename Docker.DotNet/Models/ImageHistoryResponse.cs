using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
	[DataContract]
	public class ImageHistoryResponse
	{

		[DataMember (Name = "Id")]
		public string Id { get; set; }

		[DataMember (Name = "Created")]
		public string Created { get; set; }

		[DataMember (Name = "CreatedBy")]
		public string CreatedBy { get; set; }

		[DataMember (Name = "Size")]
		public long Size { get; set; }

		[DataMember (Name = "Tags")]
		public IList<string> Tags { get; set; }

		public ImageHistoryResponse ()
		{
		}
	}
}

