using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Docker.DotNet
{
    internal class TimeSpanContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);

            var timeSpanAttribute = member.GetCustomAttribute<TimeSpanSerializationAttribute>(false);
            if (timeSpanAttribute != null)
            {
                if (timeSpanAttribute.TargetSerialization == SerializationTarget.Seconds)
                {
                    jsonProperty.Converter = new TimeSpanSecondsConverter();
                }
                else if (timeSpanAttribute.TargetSerialization == SerializationTarget.Nanoseconds)
                {
                    jsonProperty.Converter = new TimeSpanNanosecondsConverter();
                }
            }
            return jsonProperty;
        }
    }
}