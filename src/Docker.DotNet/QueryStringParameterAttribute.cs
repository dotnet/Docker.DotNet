using System;
using System.Linq;

#if (NETSTANDARD1_3 || NETSTANDARD1_6 || NETSTANDARD2_0)

#endif

namespace Docker.DotNet
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class QueryStringParameterAttribute : Attribute
    {
        public QueryStringParameterAttribute(string name, bool required) : this(name, required, null)
        {
        }

        public QueryStringParameterAttribute(string name, bool required, Type converterType)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (converterType != null && !converterType.GetInterfaces().Contains(typeof(IQueryStringConverter)))
            {
                throw new ArgumentException($"Provided query string converter type is not {typeof(IQueryStringConverter).FullName}", nameof(converterType));
            }

            Name = name;
            IsRequired = required;
            ConverterType = converterType;
        }

        public Type ConverterType { get; }
        public bool IsRequired { get; }
        public string Name { get; }
    }
}
