using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

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
                new JsonEnumMemberConverter<TaskState>(),
                new JsonEnumMemberConverter<RestartPolicyKind>(),
                new JsonDateTimeConverter(),
                new JsonNullableDateTimeConverter(),
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

        public T DeserializeObject<T>(byte[] json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, _options);
        }

        public byte[] SerializeObject<T>(T value)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(value, _options);
        }

        public JsonContent GetHttpContent<T>(T value)
        {
            return JsonContent.Create(value, options: _options);
        }

        public async Task<T> DeserializeAsync<T>(HttpContent content, CancellationToken token)
        {
            return await content.ReadFromJsonAsync<T>(_options, token)
                .ConfigureAwait(false);
        }
    }
}
