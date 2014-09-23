using System;
using System.Globalization;

namespace Docker.DotNet.Models
{
    internal class BoolQueryStringConverter : IQueryStringConverter
    {
        public BoolQueryStringConverter()
        {
        }

        public bool CanConvert(Type t)
        {
            return t == typeof (bool);
        }

        public string Convert(object o)
        {
            bool val = System.Convert.ToBoolean(o);
            int intval = val ? 1 : 0;
            return intval.ToString(CultureInfo.InvariantCulture);
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