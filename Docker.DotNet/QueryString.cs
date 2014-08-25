using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace Docker.DotNet
{
	internal class QueryString<T> : IQueryString
	{
		private static IDictionary<Type, IQueryStringConverter> ConverterInstanceRegistry = new Dictionary<Type, IQueryStringConverter> ();

		private T Object { get; set; }

		private Tuple<PropertyInfo, QueryStringParameterAttribute>[] AttributedPublicProperties { get; set; }

		public QueryString (T value)
		{
			if (EqualityComparer<T>.Default.Equals (value)) {
				throw new ArgumentNullException ("value");
			}

			this.Object = value;
			this.AttributedPublicProperties = FindAttributedPublicProperties<T, QueryStringParameterAttribute> ();
		}

		public IDictionary<string, string> GetKeyValuePairs ()
		{
			Dictionary<string, string> queryParameters = new Dictionary<string, string> ();
			foreach (var pair in this.AttributedPublicProperties) {
				PropertyInfo property = pair.Item1;
				QueryStringParameterAttribute attribute = pair.Item2;
				object value = property.GetValue (this.Object);

				// 'Required' check
				if (attribute.IsRequired && value == null) {
					string propertyFullName = string.Format (CultureInfo.InvariantCulture, "{0}.{1}", property.GetType ().FullName, property.Name);
					throw new ArgumentException ("Got null/unset value for a required query parameter.", propertyFullName);
				}

				// Serialize
				if (value != null) {
					string keyStr = attribute.Name;
					string valueStr;
					if (attribute.ConverterType == null) {
						valueStr = value.ToString ();
					} else {
						IQueryStringConverter converter = InitializeConverterType (attribute.ConverterType);
						valueStr = this.ConvertValue (converter, value);

						if (converter.ChangesKey ()) {
							keyStr = converter.GetKey (value);
						}

						if (valueStr == null) {
							throw new InvalidOperationException (string.Format (CultureInfo.InvariantCulture,
								"Got null from value converter '{0}'", attribute.ConverterType.FullName));
						}
					}


					queryParameters [attribute.Name] = valueStr;
				}
			}

			return queryParameters;
		}

		public string GetQueryString ()
		{
			return string.Join ("&", GetKeyValuePairs ().Select (pair => string.Format (CultureInfo.InvariantCulture, "{0}={1}", 
				pair.Key, Uri.EscapeUriString (pair.Value.ToString ()))));
		}

		private string ConvertValue (IQueryStringConverter converter, object value)
		{
			if (!converter.CanConvert (value.GetType ())) {
				throw new InvalidOperationException (string.Format (CultureInfo.InvariantCulture, "Cannot convert type {0} using {1}.",
					value.GetType ().FullName, converter.GetType ().FullName));
			}
			return converter.Convert (value);
		}

		private IQueryStringConverter InitializeConverterType (Type t)
		{
			if (!ConverterInstanceRegistry.ContainsKey (t)) {
				var instance = Activator.CreateInstance (t) as IQueryStringConverter;
				if (instance == null) {
					throw new InvalidOperationException (string.Format (CultureInfo.InvariantCulture, 
						"Could not get instance of {0}", t.FullName));
				}
				// Cache the instance
				ConverterInstanceRegistry [t] = instance;
			}

			return ConverterInstanceRegistry [t];
		}

		private Tuple<PropertyInfo, TAttribType>[] FindAttributedPublicProperties<TValue, TAttribType> () where TAttribType : Attribute
		{
			Type t = typeof(TValue);
			Type ofAttributeType = typeof(TAttribType);

			PropertyInfo[] properties = t.GetProperties ();
			IEnumerable<PropertyInfo> publicProperties = properties.Where (p => p.GetGetMethod (false).IsPublic);
			if (!publicProperties.Any ()) {
				throw new InvalidOperationException (string.Format (CultureInfo.InvariantCulture,
					"No public property getters found on type {0}.", t.FullName));
			}

			IEnumerable<PropertyInfo> attributedPublicProperties = properties.Where (p => Attribute.GetCustomAttribute (p, ofAttributeType) != null);
			if (!attributedPublicProperties.Any ()) {
				throw new InvalidOperationException (string.Format (CultureInfo.InvariantCulture,
					"No public properties attributed with [{0}] found on type {1}.", ofAttributeType.FullName, t.FullName));
			}

			return attributedPublicProperties.Select (pi =>
				new Tuple<PropertyInfo, TAttribType> (pi, Attribute.GetCustomAttribute (pi, ofAttributeType) as TAttribType)).ToArray ();
		}
	}
}
