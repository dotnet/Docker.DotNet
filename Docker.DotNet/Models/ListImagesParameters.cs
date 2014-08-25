using System;
using System.Collections.Generic;

namespace Docker.DotNet.Models
{
	public class ListImagesParameters
	{
		public bool? All { get; set; }

		public IDictionary<string, IList<string>> Filters { get; set; }

		public ListImagesParameters ()
		{
		}
	}
}

