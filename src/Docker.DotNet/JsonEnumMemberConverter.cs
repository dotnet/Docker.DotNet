using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Docker.DotNet;

// Adapted from https://github.com/dotnet/runtime/issues/74385#issuecomment-1705083109
internal sealed class JsonEnumMemberConverter<TEnum> : JsonStringEnumConverter<TEnum> where TEnum : struct, Enum
{
    public JsonEnumMemberConverter() : base(namingPolicy: ResolveNamingPolicy())
    {
    }

    private static JsonNamingPolicy ResolveNamingPolicy()
    {
        var map = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => (f.Name, AttributeName: f.GetCustomAttribute<EnumMemberAttribute>()?.Value))
            .Where(pair => pair.AttributeName != null)
            .ToDictionary(e => e.Name, e => e.AttributeName);

        return map.Count > 0 ? new EnumMemberNamingPolicy(map) : null;
    }

    private sealed class EnumMemberNamingPolicy : JsonNamingPolicy
    {
        private readonly IReadOnlyDictionary<string, string> _map;

        public EnumMemberNamingPolicy(IReadOnlyDictionary<string, string> map) => _map = map;

        public override string ConvertName(string name) => _map.TryGetValue(name, out var newName) ? newName : name;
    }
}