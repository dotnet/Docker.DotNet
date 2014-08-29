using System;
using System.Globalization;

namespace Docker.DotNet
{
    internal static class HttpUtility
    {
        public static Uri BuildUri(Uri baseUri, Version requestedApiVersion, string path, IQueryString queryString)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException("baseUri");
            }

            UriBuilder builder = new UriBuilder(baseUri);

            if (requestedApiVersion != null)
            {
                builder.Path += string.Format(CultureInfo.InvariantCulture, "v{0}/", requestedApiVersion);
            }

            if (!string.IsNullOrEmpty(path))
            {
                builder.Path += path;
            }

            if (queryString != null)
            {
                builder.Query = queryString.GetQueryString();
            }

            return builder.Uri;
        }
    }
}