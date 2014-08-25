using System;

namespace Docker.DotNet.Models
{
	public class StopContainerParameters
	{
		public TimeSpan? Wait { get; set; }

		public StopContainerParameters ()
		{
		}
	}
}

