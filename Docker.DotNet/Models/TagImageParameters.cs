using System;

namespace Docker.DotNet.Models
{
	public class TagImageParameters
	{
		[QueryStringParameterAttribute ("repo", true)]
		public string Repo { get; set; }

		[QueryStringParameterAttribute ("force", false, typeof(BoolQueryStringConverter))]
		public bool? Force { get; set; }

		public TagImageParameters ()
		{
		}
	}
}

