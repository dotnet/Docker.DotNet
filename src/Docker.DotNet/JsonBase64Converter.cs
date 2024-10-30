using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Docker.DotNet
{
    internal class JsonBase64Converter : JsonConverter<IList<byte>>
    {
        public override IList<byte> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetBytesFromBase64();
        }

        public override void Write(Utf8JsonWriter writer, IList<byte> value, JsonSerializerOptions options)
        {
            var bytes = GetBytes(value);
            writer.WriteBase64StringValue(bytes);
        }

        private static ReadOnlySpan<byte> GetBytes(IList<byte> value)
        {
#if !NETSTANDARD
            if (value is List<byte> list)
            {
                return CollectionsMarshal.AsSpan(list);
            }
#endif
            if (value is byte[] array)
            {
                return array;
            }

            return value.ToArray();
        }
    }
}