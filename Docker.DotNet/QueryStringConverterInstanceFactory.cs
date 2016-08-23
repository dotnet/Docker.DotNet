using System;
using System.Collections.Generic;

namespace Docker.DotNet
{
    internal class QueryStringConverterInstanceFactory : IQueryStringConverterInstanceFactory
    {
        private static readonly IDictionary<Type, IQueryStringConverter> ConverterInstanceRegistry;

        static QueryStringConverterInstanceFactory()
        {
            ConverterInstanceRegistry = new Dictionary<Type, IQueryStringConverter>();
        }

        public IQueryStringConverter GetConverterInstance(Type t)
        {
            var cached = GetCachedInstance(t);
            if (cached != null)
            {
                return cached;
            }

            // Create new instance
            var instance = InitializeConverter(t);

            // Cache the instance
            CacheInstance(t, instance);
            return instance;
        }

        private IQueryStringConverter InitializeConverter(Type t)
        {
            var instance = Activator.CreateInstance(t) as IQueryStringConverter;
            if (instance == null)
            {
                throw new InvalidOperationException($"Could not get instance of {t.FullName}");
            }
            return instance;
        }

        private static IQueryStringConverter GetCachedInstance(Type t)
        {
            return (ConverterInstanceRegistry.ContainsKey(t)) ? ConverterInstanceRegistry[t] : null;
        }

        private static void CacheInstance(Type t, IQueryStringConverter instance)
        {
            ConverterInstanceRegistry[t] = instance;
        }
    }
}