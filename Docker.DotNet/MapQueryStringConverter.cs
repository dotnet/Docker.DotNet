using System;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Docker.DotNet
{
    internal class MapQueryStringConverter : IQueryStringConverter
    {
        public bool CanConvert(Type t)
        {
            return typeof (IList).IsAssignableFrom(t) || typeof (IDictionary).IsAssignableFrom(t);
        }

        public string[] Convert(object o)
        {
            Debug.Assert(o != null);

            return new [] {JsonConvert.SerializeObject(o)};
        }
    }
}