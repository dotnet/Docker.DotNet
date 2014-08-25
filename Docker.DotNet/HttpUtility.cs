using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Docker.DotNet
{
	internal static class HttpUtility
	{
		public static Uri BuildUri (Uri baseUri, string path, IQueryString queryString)
		{
			UriBuilder builder = new UriBuilder (baseUri);
			builder.Path += path;
			builder.Query = queryString.GetQueryString ();
			return builder.Uri;
		}
	}
}

