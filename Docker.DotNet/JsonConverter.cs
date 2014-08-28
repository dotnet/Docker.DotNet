using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Docker.DotNet
{
    /// <summary>
    /// Facade for <see cref="Newtonsoft.Json.JsonConvert"/>.
    /// </summary>
    internal class JsonConverter
    {
        private Newtonsoft.Json.JsonConverter[] Converters { get; set; }

        public JsonConverter()
        {
            this.Converters = new Newtonsoft.Json.JsonConverter[] {new IsoDateTimeConverter()};
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