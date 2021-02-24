using System;

namespace Domain.Common
{
    public static class EnumExtensions
    {
        public static T ParseEnumFromString<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }
    }
}