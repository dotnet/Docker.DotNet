using System;
using System.Diagnostics;
using System.Globalization;

namespace Docker.DotNet.Models
{
    internal class BoolQueryStringConverter : IQueryStringConverter
    {
        public bool CanConvert(Type t)
        {
            return t == typeof (bool);
        }

        public string[] Convert(object o)
        {
            Debug.Assert(o != null);

            return new[] {System.Convert.ToInt32(System.Convert.ToBoolean(o)).ToString(CultureInfo.InvariantCulture)};
        }
    }
}