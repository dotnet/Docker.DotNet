using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace Docker.DotNet
{
    internal class MapQueryStringConverter : IQueryStringConverter
    {
        public bool CanConvert(Type t)
        {
            return typeof(IList).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()) || typeof(IDictionary).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo());
        }

        public string[] Convert(object o)
        {
            Debug.Assert(o != null);

            return new[] { System.Text.Json.JsonSerializer.Serialize(o) };
        }
    }
}