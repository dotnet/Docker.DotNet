using System;

namespace Docker.DotNet.Models
{
	public class CommitContainerChangesParameters
	{
		[QueryStringParameterAttribute ("container", true)]
		public string ContainerId { get; set; }

		[QueryStringParameterAttribute ("repo", false)]
		public string Repo { get; set; }

		[QueryStringParameterAttribute ("tag", false)]
		public string Tag  { get; set; }

		[QueryStringParameterAttribute ("m", false)]
		public string Message  { get; set; }

		[QueryStringParameterAttribute ("author", false)]
		public string Author  { get; set; }

		public Config Config { get; set; }

		public CommitContainerChangesParameters ()
		{
		}
	}
}

