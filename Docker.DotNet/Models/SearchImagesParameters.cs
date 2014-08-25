using System;

namespace Docker.DotNet.Models
{
	public class SearchImagesParameters
	{
		[QueryStringParameterAttribute ("term", true)]
		public string Term { get; set; }

		public SearchImagesParameters ()
		{
		}
	}
}

