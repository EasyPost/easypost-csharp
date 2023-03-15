using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities.Internal.Attributes
{
    internal abstract class BaseCustomAttribute : Attribute, IBaseCustomAttribute
    {
        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type @type)
        {
            List<PropertyInfo> matchingProperties = new();

            PropertyInfo[] properties = @type.GetProperties(BindingFlags.Instance |
                                                            BindingFlags.Static |
                                                            BindingFlags.NonPublic |
                                                            BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                object[] attributes = property.GetCustomAttributes(typeof(T), true);

                if (attributes.Any())
                {
                    matchingProperties.Add(property);
                }
            }

            return matchingProperties;
        }

        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj)
            where T : Attribute
            => GetPropertiesWithAttribute<T>(obj.GetType());

        internal static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(Type @type)
        {
            List<MethodInfo> matchingMethods = new();

            MethodInfo[] methods = @type.GetMethods(BindingFlags.Instance |
                                                    BindingFlags.Static |
                                                    BindingFlags.NonPublic |
                                                    BindingFlags.Public);

            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(typeof(T), true);

                if (attributes.Any())
                {
                    matchingMethods.Add(method);
                }
            }

            return matchingMethods;
        }

        internal static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(object obj)
            where T : Attribute
            => GetMethodsWithAttribute<T>(obj.GetType());

        /// <summary>
        ///     Get the attribute of the specified type for a property.
        /// </summary>
        /// <param name="property">Property to get attribute of.</param>
        /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
        /// <returns>T-type attribute for the property.</returns>
        public static T? GetAttribute<T>(PropertyInfo property)
            where T : BaseCustomAttribute
        {
            try
            {
                return property.GetCustomAttribute<T>(true);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception)
            {
                return default;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }

    internal interface IBaseCustomAttribute
    {
    }
}
