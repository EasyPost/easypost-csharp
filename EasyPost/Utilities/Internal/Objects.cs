using System;
using System.Reflection;

namespace EasyPost.Utilities.Internal
{
    /// <summary>
    ///     Utility methods related to general objects in .NET.
    /// </summary>
    public static class Objects
    {
        /// <summary>
        ///     Check if a property of a specific object exists and is not null.
        /// </summary>
        /// <param name="obj">The object to check properties of.</param>
        /// <param name="propertyName">The name of the property to search for and evaluate.</param>
        /// <returns><c>true</c> if the property exists on the object and is not null, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If a property of the provided name does not exist on the provided object.</exception>
        public static bool IsPropertySet(object obj, string propertyName)
        {
            PropertyInfo? otherProperty = obj.GetType().GetProperty(propertyName);
            return otherProperty == null
                ? throw new ArgumentException($"Property {propertyName} does not exist on object {obj.GetType().Name}")
                : otherProperty.GetValue(obj) != null;
        }

        /// <summary>
        ///     Check if an object is a primitive type (e.g. boolean, integer, string) or <c>null</c>.
        /// </summary>
        /// <param name="obj">The object to evaluate.</param>
        /// <returns><c>true</c> if the object is a primitive type or <c>null</c>, <c>false</c> otherwise.</returns>
        public static bool IsPrimitive(object? obj)
        {
            return obj is string or ValueType or null;
        }
    }
}
