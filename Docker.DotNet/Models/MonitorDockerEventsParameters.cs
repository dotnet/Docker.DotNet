using System;

namespace Docker.DotNet.Models
{
	public class MonitorDockerEventsParameters
	{
		[QueryStringParameterAttribute ("since", false)]
		public long? Since { get; set; }

		[QueryStringParameterAttribute ("until", false)]
		public long? Until { get; set; }

		public MonitorDockerEventsParameters ()
		{
		}
	}
}

