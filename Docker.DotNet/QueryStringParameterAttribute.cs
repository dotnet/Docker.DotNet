using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Docker.DotNet
{
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property)]
    internal class QueryStringParameterAttribute : Attribute
    {
        public string Name { get; private set; }

        public bool IsRequired { get; private set; }

        public Type ConverterType { get; private set; }

        public QueryStringParameterAttribute(string name, bool required) : this(name, required, null)
        {
        }

        public QueryStringParameterAttribute(string name, bool required, Type converterType)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (converterType != null && !converterType.GetInterfaces().Contains(typeof (IQueryStringConverter)))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Provided query string converter type is not {0}", typeof (IQueryStringConverter).FullName), "converterType");
            }

            this.Name = name;
            this.IsRequired = required;
            this.ConverterType = converterType;
        }
    }
}