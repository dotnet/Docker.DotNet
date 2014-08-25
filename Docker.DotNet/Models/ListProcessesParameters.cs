using System;

namespace Docker.DotNet.Models
{
	public class ListProcessesParameters
	{
		[QueryStringParameterAttribute ("ps_args", false)]
		public string PsArgs { get; set; }

		public ListProcessesParameters ()
		{
		}
	}
}

