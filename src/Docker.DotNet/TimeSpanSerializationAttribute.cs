using System;

namespace Docker.DotNet
{    
    internal enum SerializationTarget
    {
        Seconds,
        Nanoseconds
    }

    [AttributeUsage(validOn: AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class TimeSpanSerializationAttribute : Attribute
    {
        public SerializationTarget TargetSerialization { get; private set; }

        public TimeSpanSerializationAttribute(SerializationTarget target)
        {
            TargetSerialization = target;
        }
    }
}