using System;

namespace Docker.DotNet.Models
{
	public class StopContainerParameters
	{
		[QueryStringParameterAttribute ("t", false, typeof(TimeSpanSecondsQueryStringConverter))]
		public TimeSpan? Wait { get; set; }

		public StopContainerParameters ()
		{
		}
	}
}

