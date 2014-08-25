using System;

namespace Docker.DotNet.Models
{
	public class BuildImageFromDockerfileParameters
	{
		public string RepositoryTagName { get; set; }

		public bool? Quiet { get; set; }

		public bool? NoCache { get; set; }

		public bool? RemoveIntermediateContainers { get; set; }

		public bool? ForceRemoveIntermediateContainers { get; set; }

		public BuildImageFromDockerfileParameters ()
		{
		}
	}
}

