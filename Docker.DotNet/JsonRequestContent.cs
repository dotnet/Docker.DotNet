using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Docker.DotNet
{
    internal class JsonRequestContent<T> : IRequestContent
    {
        private const string JsonMimeType = "application/json";

        private T Value { get; set; }

        private JsonSerializer Serializer { get; set; }

        public JsonRequestContent(T val, JsonSerializer serializer)
        {
            if (EqualityComparer<T>.Default.Equals(val))
            {
                throw new ArgumentNullException("val");
            }

            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            this.Value = val;
            this.Serializer = serializer;
        }

        public HttpContent GetContent()
        {
            string serializedObject = this.Serializer.SerializeObject(this.Value);
            return new StringContent(serializedObject, Encoding.UTF8, JsonMimeType);
        }
    }
}