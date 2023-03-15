using System;
using System.Reflection;

namespace EasyPost.Utilities.Internal
{
    public static class Objects
    {
        public static bool IsPropertySet(object obj, string propertyName)
        {
            PropertyInfo? otherProperty = obj.GetType().GetProperty(propertyName);
            return otherProperty == null
                ? throw new ArgumentException($"Property {propertyName} does not exist on object {obj.GetType().Name}")
                : otherProperty.GetValue(obj) != null;
        }

        public static bool IsPrimitive(object? obj)
        {
            return obj is string or ValueType or null;
        }
    }
}
