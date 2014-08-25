using System;
using System.Runtime.Serialization;

namespace Docker.DotNet.Models
{
	public class GetContainerLogsParameters
	{
		[QueryStringParameterAttribute ("follow", false, typeof(BoolQueryStringConverter))]
		public bool? Follow { get; set; }

		[QueryStringParameterAttribute ("stdout", false, typeof(BoolQueryStringConverter))]
		public bool? Stdout { get; set; }

		[QueryStringParameterAttribute ("stderr", false, typeof(BoolQueryStringConverter))]
		public bool? Stderr { get; set; }

		[QueryStringParameterAttribute ("timestamps", false, typeof(BoolQueryStringConverter))]
		public bool? Timestamps { get; set; }

		[QueryStringParameterAttribute ("tail", false, typeof(ContainerLogsTailModeQueryStringConverter))]
		public IContainerLogsTailMode Tail { get; set; }

		public GetContainerLogsParameters ()
		{
		}
	}
}

