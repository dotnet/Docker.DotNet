using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Docker.DotNet
{
    /// <summary>
    /// Facade for <see cref="System.Text.Json.JsonSerializer"/> serialization.
    /// </summary>
    internal class JsonSerializer
    {
        private readonly JsonSerializerOptions _options = new()
        {
            Converters =
            {
                new JsonDateTimeConverter(),
                new JsonNullableDateTimeConverter(),
                new JsonStringEnumConverter(),
                new JsonBase64Converter(),
            },
        };

        // Adapted from https://github.com/dotnet/runtime/issues/33030#issuecomment-1524227075
        public async IAsyncEnumerable<T> Deserialize<T>(Stream stream, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var reader = PipeReader.Create(stream);
            while (true)
            {
                var result = await reader.ReadAsync(cancellationToken);
                var buffer = result.Buffer;
                while (!buffer.IsEmpty && TryParseJson(ref buffer, out var jsonDocument))
                {
                    yield return jsonDocument.Deserialize<T>(_options);
                }

                if (result.IsCompleted)
                {
                    break;
                }

                reader.AdvanceTo(buffer.Start, buffer.End);
            }

            await reader.CompleteAsync();
        }

        private static bool TryParseJson(ref ReadOnlySequence<byte> buffer, out JsonDocument jsonDocument)
        {
            var reader = new Utf8JsonReader(buffer, isFinalBlock: false, default);

            if (JsonDocument.TryParseValue(ref reader, out jsonDocument))
            {
                buffer = buffer.Slice(reader.BytesConsumed);
                return true;
            }

            return false;
        }

        public T DeserializeObject<T>(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, _options);
        }

        public string SerializeObject<T>(T value)
        {
            return System.Text.Json.JsonSerializer.Serialize(value, _options);
        }

        public JsonContent GetHttpContent<T>(T value)
        {
            return JsonContent.Create(value, options: _options);
        }
    }
}
