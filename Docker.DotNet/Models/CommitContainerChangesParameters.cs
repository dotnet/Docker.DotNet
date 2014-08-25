using System;

namespace Docker.DotNet.Models
{
	public class CommitContainerChangesParameters
	{
		public string ContainerId { get; set; }

		public string Repo { get; set; }

		public string Tag  { get; set; }

		public string Message  { get; set; }

		public string Author  { get; set; }

		public Config Config { get; set; }

		public CommitContainerChangesParameters ()
		{
		}
	}
}

