using System;

namespace Docker.DotNet.Models
{
	public class BuildImageFromDockerfileParameters
	{
		[QueryStringParameterAttribute ("t", false)]
		public string RepositoryTagName { get; set; }

		[QueryStringParameterAttribute ("q", false, typeof(BoolQueryStringConverter))]
		public bool? Quiet { get; set; }

		[QueryStringParameterAttribute ("nocache", false, typeof(BoolQueryStringConverter))]
		public bool? NoCache { get; set; }

		[QueryStringParameterAttribute ("rm", false, typeof(BoolQueryStringConverter))]
		public bool? RemoveIntermediateContainers { get; set; }

		[QueryStringParameterAttribute ("forcerm", false, typeof(BoolQueryStringConverter))]
		public bool? ForceRemoveIntermediateContainers { get; set; }

		public BuildImageFromDockerfileParameters ()
		{
		}
	}
}

