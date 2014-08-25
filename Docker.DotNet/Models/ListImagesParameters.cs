using System;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
	public class ListImagesParameters
	{
		[QueryStringParameterAttribute ("all", false, typeof(BoolQueryStringConverter))]
		public bool? All { get; set; }

		[QueryStringParameterAttribute ("filters", false, typeof(JsonQueryStringConverter))]
		public IDictionary<string, IList<string>> Filters { get; set; }

		public ListImagesParameters ()
		{
		}
	}
}

