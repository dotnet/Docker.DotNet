using System;

namespace Docker.DotNet.Models
{
	public class RestartContainerParameters
	{
		[QueryStringParameterAttribute ("t", false, typeof(TimeSpanSecondsQueryStringConverter))]
		public TimeSpan? Wait { get; set; }

		public RestartContainerParameters ()
		{
		}
	}
}

