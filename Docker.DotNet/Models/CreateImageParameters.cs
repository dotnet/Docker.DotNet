using System;

namespace Docker.DotNet.Models
{
	public class CreateImageParameters
	{
		[QueryStringParameterAttribute ("fromImage", false)]
		public string FromImage { get; set; }

		[QueryStringParameterAttribute ("repo", false)]
		public string Repo { get; set; }

		[QueryStringParameterAttribute ("tag", false)]
		public string Tag { get; set; }

		[QueryStringParameterAttribute ("registry", false)]
		public string Registry { get; set; }

		public CreateImageParameters ()
		{
		}
	}
}

