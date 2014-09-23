using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Docker.DotNet
{
    internal class JsonVersionConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            string strVal = reader.Value as string;
            if (strVal == null)
            {
                var valueType = reader.Value == null ? "<null>" : reader.Value.GetType().FullName;
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot deserialize value of type '{0}' to '{1}' ", valueType, objectType.FullName));
            }

            return Version.Parse(strVal);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Version);
        }
    }
}