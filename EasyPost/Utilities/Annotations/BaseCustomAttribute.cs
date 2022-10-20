using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities.Annotations
{
    internal abstract class BaseCustomAttribute : Attribute, IBaseCustomAttribute
    {
        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type @type)
        {
            var matchingProperties = new List<PropertyInfo>();

            var properties = @type.GetProperties();

            foreach (var property in properties)
            {
                object[] attributes = property.GetCustomAttributes(typeof(T), true);

                if (attributes.Any())
                {
                    matchingProperties.Add(property);
                }
            }

            return matchingProperties;
        }

        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj) where T : Attribute
        {
            return GetPropertiesWithAttribute<T>(obj.GetType());
        }

        internal static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(Type @type)
        {
            var matchingMethods = new List<MethodInfo>();

            var methods = @type.GetMethods();

            foreach (var method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(T), true);

                if (attributes.Any())
                {
                    matchingMethods.Add(method);
                }
            }

            return matchingMethods;
        }

        internal static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(object obj) where T : Attribute
        {
            return GetMethodsWithAttribute<T>(obj.GetType());
        }
    }

    internal interface IBaseCustomAttribute
    {
    }
}
