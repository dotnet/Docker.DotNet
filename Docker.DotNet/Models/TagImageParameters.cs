using System;

namespace Docker.DotNet.Models
{
	public class TagImageParameters
	{
		public string Repo { get; set; }

		public bool? Force { get; set; }

		public TagImageParameters ()
		{
		}
	}
}

