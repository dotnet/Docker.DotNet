using System;

namespace Docker.DotNet.Models
{
	public class RemoveContainerParameters
	{
		[QueryStringParameterAttribute ("v", false)]
		public bool? RemoveVolumes { get; set; }

		[QueryStringParameterAttribute ("force", false)]
		public bool? Force { get; set; }

		public RemoveContainerParameters ()
		{
		}
	}
}

