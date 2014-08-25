using System;

namespace Docker.DotNet.Models
{
	public class PushImageParameters
	{
		[QueryStringParameterAttribute ("tag", false)]
		public string Tag { get; set; }

		public PushImageParameters ()
		{
		}
	}
}

