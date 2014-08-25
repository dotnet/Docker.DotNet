using System;

namespace Docker.DotNet.Models
{
	public class RestartContainerParameters
	{
		public TimeSpan? Wait { get; set; }

		public RestartContainerParameters ()
		{
		}
	}
}

