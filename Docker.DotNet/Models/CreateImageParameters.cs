using System;

namespace Docker.DotNet.Models
{
	public class CreateImageParameters
	{
		public string Repo { get; set; }

		public string FromImage { get; set; }

		public string Tag { get; set; }

		public string Registry { get; set; }

		public CreateImageParameters ()
		{
		}
	}
}

