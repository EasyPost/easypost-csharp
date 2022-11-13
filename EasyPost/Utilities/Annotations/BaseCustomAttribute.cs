using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities.Annotations;

internal abstract class BaseCustomAttribute : Attribute, IBaseCustomAttribute
{
    internal static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(Type @type)
    {
        List<MethodInfo>? matchingMethods = new List<MethodInfo>();

        MethodInfo[]? methods = @type.GetMethods();

        foreach (MethodInfo? method in methods)
        {
            object[] attributes = method.GetCustomAttributes(typeof(T), true);

            if (attributes.Any())
            {
                matchingMethods.Add(method);
            }
        }

        return matchingMethods;
    }

    internal static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(object obj) where T : Attribute => GetMethodsWithAttribute<T>(obj.GetType());

    internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type @type)
    {
        List<PropertyInfo>? matchingProperties = new List<PropertyInfo>();

        PropertyInfo[]? properties = @type.GetProperties();

        foreach (PropertyInfo? property in properties)
        {
            object[] attributes = property.GetCustomAttributes(typeof(T), true);

            if (attributes.Any())
            {
                matchingProperties.Add(property);
            }
        }

        return matchingProperties;
    }

    internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj) where T : Attribute => GetPropertiesWithAttribute<T>(obj.GetType());
}

internal interface IBaseCustomAttribute
{
}
