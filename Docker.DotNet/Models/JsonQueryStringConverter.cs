using System;
using Newtonsoft.Json;

namespace Docker.DotNet.Models
{
    internal class JsonQueryStringConverter : IQueryStringConverter
    {
        public JsonQueryStringConverter()
        {
        }

        public bool CanConvert(Type t)
        {
            return true;
        }

        public string Convert(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.None);
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