using System;
using Newtonsoft.Json;

namespace Docker.DotNet
{
    internal class TimeSpanSecondsConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var timeSpan = value as TimeSpan?;
            if (timeSpan == null)
            {
                return;
            }

            writer.WriteValue(timeSpan.Value.TotalSeconds);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var valueInSeconds = reader.ReadAsInt32();
            if (!valueInSeconds.HasValue)
            {
                return null;
            }

            return TimeSpan.FromSeconds(valueInSeconds.Value);
        }
    }
}