using System;
using System.Text.Json;

namespace Docker.DotNet;

internal class JsonDateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
{
    private static readonly DateTime UnixEpochBase = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => UnixEpochBase.AddSeconds(reader.GetInt64()),
            JsonTokenType.String => reader.GetDateTime(),
            _ => throw new NotImplementedException($"Deserializing a JSON {reader.TokenType} to DateTime is not handled.")
        };
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}