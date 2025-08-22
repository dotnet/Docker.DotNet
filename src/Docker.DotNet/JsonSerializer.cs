using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Docker.DotNet
{
    /// <summary>
    /// Facade for <see cref="System.Text.Json.JsonSerializer"/> serialization.
    /// </summary>
    internal class JsonSerializer
    {
        public JsonSerializer()
        {
            DockerDotnetJsonSerializerContext.PreserveReflection();
        }

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
                    var deserializedObj = jsonDocument.Deserialize(typeof(T), DockerDotnetJsonSerializerContext.Default);
                    yield return (T) deserializedObj;
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
            var deserializedObj = System.Text.Json.JsonSerializer.Deserialize(json, typeof(T), DockerDotnetJsonSerializerContext.Default);
            return (T)deserializedObj;
        }

        public byte[] SerializeObject<T>(T value)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(value, typeof(T), DockerDotnetJsonSerializerContext.Default);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public HttpContent GetHttpContent<T>(T value)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(value, typeof(T), DockerDotnetJsonSerializerContext.Default);
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return httpContent;
        }

        public async Task<T> DeserializeAsync<T>(HttpContent content, CancellationToken token)
        {
            var jsonString = await content.ReadAsStringAsync(token);
            var deserializedObj = System.Text.Json.JsonSerializer.Deserialize(jsonString, typeof(T), DockerDotnetJsonSerializerContext.Default);
            return (T)deserializedObj;
        }
    }
}
