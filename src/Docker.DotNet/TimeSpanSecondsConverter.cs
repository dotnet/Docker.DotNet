using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Docker.DotNet
{
    internal class TimeSpanSecondsConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            return TimeSpan.FromSeconds(reader.GetInt64());
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                return;
            }

            writer.WriteNumberValue((long)value.Value.TotalSeconds);
        }
    }
}