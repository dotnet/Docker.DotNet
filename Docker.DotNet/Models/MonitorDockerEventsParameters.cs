using System;

namespace Docker.DotNet.Models
{
	public class MonitorDockerEventsParameters
	{
		public long? Since { get; set; }

		public long? Until { get; set; }

		public MonitorDockerEventsParameters ()
		{
		}
	}
}

