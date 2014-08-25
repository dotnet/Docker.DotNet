using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Docker.DotNet
{
	internal static class HttpUtility
	{
		public static string BuildQueryString(IDictionary<string,object> queryParameters){
			if (queryParameters == null || queryParameters.Count == 0) {
				return "";
			}

			return string.Join ("&", queryParameters.Select (pair => string.Format (CultureInfo.InvariantCulture, "{0}={1}", pair.Key, Uri.EscapeUriString (pair.Value.ToString()))));
		}

		public static Uri BuildUri(Uri baseUri, string path, IDictionary<string,object> queryParameters){
			UriBuilder builder = new UriBuilder (baseUri);
			builder.Path += path;
			builder.Query = HttpUtility.BuildQueryString (queryParameters);
			return builder.Uri;
		}
	}
}

