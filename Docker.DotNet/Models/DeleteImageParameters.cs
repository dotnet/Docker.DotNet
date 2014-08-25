using System;

namespace Docker.DotNet.Models
{
	public class DeleteImageParameters
	{
		[QueryStringParameterAttribute ("force", false, typeof(BoolQueryStringConverter))]
		public bool? Force { get; set; }

		[QueryStringParameterAttribute ("noprune", false, typeof(BoolQueryStringConverter))]
		public bool? NoPrune { get; set; }

		public DeleteImageParameters ()
		{
		}
	}
}

