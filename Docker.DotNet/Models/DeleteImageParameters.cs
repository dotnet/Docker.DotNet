using System;

namespace Docker.DotNet.Models
{
	public class DeleteImageParameters
	{
		public bool? Force { get; set; }

		public bool? NoPrune { get; set; }

		public DeleteImageParameters ()
		{
		}
	}
}

