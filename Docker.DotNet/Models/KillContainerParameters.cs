using System;

namespace Docker.DotNet.Models
{
	public class KillContainerParameters
	{
		[QueryStringParameterAttribute ("signal", false)]
		public string Signal { get; set; }

		public KillContainerParameters ()
		{
		}
	}
}

