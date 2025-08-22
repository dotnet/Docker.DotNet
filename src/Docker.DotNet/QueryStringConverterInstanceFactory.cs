using System;
using System.Collections.Concurrent;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class QueryStringConverterInstanceFactory : IQueryStringConverterInstanceFactory
    {
        private static readonly ConcurrentDictionary<Type, IQueryStringConverter> ConverterInstanceRegistry = new ConcurrentDictionary<Type, IQueryStringConverter>();

        public IQueryStringConverter GetConverterInstance(Type t)
        {
            return ConverterInstanceRegistry.GetOrAdd(t, InitializeConverter);
        }

        private IQueryStringConverter InitializeConverter(Type t)
        {
            return t.Name switch
            {
                nameof(BoolQueryStringConverter) => new BoolQueryStringConverter(),
                nameof(EnumerableQueryStringConverter) => new EnumerableQueryStringConverter(),
                nameof(MapQueryStringConverter) => new MapQueryStringConverter(),
                nameof(TimeSpanSecondsQueryStringConverter) => new TimeSpanSecondsQueryStringConverter(),
                _ => throw new InvalidOperationException($"Could not get instance of {t.FullName}")
            };
        }
    }
}