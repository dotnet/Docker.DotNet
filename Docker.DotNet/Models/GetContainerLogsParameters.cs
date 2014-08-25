using System;

namespace Docker.DotNet.Models
{
	public class GetContainerLogsParameters
	{
		public bool? Follow { get; set; }

		public bool? Stdout { get; set; }

		public bool? Stderr { get; set; }

		public bool? Timestamps { get; set; }

		public IContainerLogsTailMode Tail { get; set; }

		public GetContainerLogsParameters ()
		{

		}
	}
}

