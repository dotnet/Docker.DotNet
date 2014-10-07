using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Docker.DotNet
{
    /// <summary>
    /// Facade for <see cref="Newtonsoft.Json.JsonConvert"/>.
    /// </summary>
    internal class JsonSerializer
    {
        private Newtonsoft.Json.JsonConverter[] Converters { get; set; }

        public JsonSerializer()
        {
            this.Converters = new Newtonsoft.Json.JsonConverter[]
            {
                new JsonIso8601AndUnixEpochDateConverter(),
                new JsonVersionConverter(),
                new StringEnumConverter()
            };
        }

        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, this.Converters);
        }

        public string SerializeObject<T>(T value)
        {
            return JsonConvert.SerializeObject(value, this.Converters);
        }
    }
}