using System;

namespace Docker.DotNet.Models
{
	public class RemoveContainerParameters
	{
		public bool? RemoveVolumes { get; set; }

		public bool? Force { get; set; }

		public RemoveContainerParameters ()
		{
		}
	}
}

