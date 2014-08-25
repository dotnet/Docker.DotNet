using System;
using Newtonsoft.Json;

namespace Docker.DotNet
{
	/// <summary>
	/// Converts UNIX epoch timestamps (in seconds) to .NET <see cref="System.DateTime"/> values.
	/// </summary>
	internal class JsonUnixDateConverter : Newtonsoft.Json.JsonConverter
	{
		public readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		public JsonUnixDateConverter ()
		{
		}


		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var t = (long)reader.Value;
			return UnixEpoch.AddSeconds(t);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}

