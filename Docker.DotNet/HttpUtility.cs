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
			if (baseUri == null) {
				throw new ArgumentNullException ("baseUri");
			}

			UriBuilder builder = new UriBuilder (baseUri);

			if (!string.IsNullOrEmpty (path)) {
				builder.Path += path;
			}

			if (queryString != null) {
				builder.Query = queryString.GetQueryString ();
			}

			return builder.Uri;
		}
	}
}

