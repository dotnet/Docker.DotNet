using System;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;

namespace Docker.DotNet
{
	internal class JsonRequestContent<T> : IRequestContent
	{
		private static string JsonMimeType = "application/json";

		private T Value { get; set; }

		private JsonConverter Serializer { get; set; }

		public JsonRequestContent (T val, JsonConverter serializer)
		{
			if (EqualityComparer<T>.Default.Equals (val)) {
				throw new ArgumentNullException ("val");
			}

			if (serializer == null) {
				throw new ArgumentNullException ("serializer");
			}

			this.Value = val;
			this.Serializer = serializer;
		}

		public HttpContent GetContent ()
		{
			string serializedObject = this.Serializer.SerializeObject<T> (this.Value);
			return new StringContent (serializedObject, Encoding.UTF8, JsonMimeType);
		}
	}
}
