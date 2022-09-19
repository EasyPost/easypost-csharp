using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities.Annotations
{
    internal abstract class BaseCustomAttribute : Attribute, IBaseCustomAttribute
    {
        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type @type) where T : Attribute
        {
            var properties = @type.GetProperties();

            return (from property in properties let attributes = property.GetCustomAttributes(typeof(T), true) where attributes.Any() select property).ToList();
        }

        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj) where T : Attribute
        {
            return GetPropertiesWithAttribute<T>(obj.GetType());
        }
    }

    internal interface IBaseCustomAttribute
    {
    }
}
