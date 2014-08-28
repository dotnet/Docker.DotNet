using System;
using System.Globalization;

namespace Docker.DotNet.Models
{
    internal class TimeSpanSecondsQueryStringConverter : IQueryStringConverter
    {
        public TimeSpanSecondsQueryStringConverter()
        {
        }

        public bool CanConvert(Type t)
        {
            return t == typeof (TimeSpan);
        }

        public string Convert(object o)
        {
            TimeSpan ts = (TimeSpan) o;
            return ts.TotalSeconds.ToString(CultureInfo.InvariantCulture);
        }

        public bool ChangesKey()
        {
            return false;
        }

        public string GetKey(object o)
        {
            throw new NotImplementedException();
        }
    }
}