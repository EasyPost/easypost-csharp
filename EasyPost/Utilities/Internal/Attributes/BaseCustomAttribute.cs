using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasyPost.Utilities.Internal.Attributes
{
    /// <summary>
    ///     The base class for all custom <see cref="Attribute"/>s in the SDK.
    /// </summary>
    internal abstract class BaseCustomAttribute : Attribute, IBaseCustomAttribute
    {
        /// <summary>
        ///     Get all properties of a given <see cref="Type"/> decorated with a T-type attribute.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to evaluate properties of.</param>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <returns>A list of all properties of the provided type decorated with the T-type attribute.</returns>
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

                if (attributes.Length > 0)
                {
                    matchingProperties.Add(property);
                }
            }

            return matchingProperties;
        }

        /// <summary>
        ///     Get all properties of a given object decorated with a T-type attribute.
        /// </summary>
        /// <param name="obj">The object to evaluate properties of.</param>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <returns>A list of all properties of the provided object decorated with the T-type attribute.</returns>
        internal static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj)
            where T : Attribute
            => GetPropertiesWithAttribute<T>(obj.GetType());

        /// <summary>
        ///     Get all methods of a given <see cref="Type"/> decorated with a T-type attribute.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to evaluate methods of.</param>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <returns>A list of all methods of the provided type decorated with the T-type attribute.</returns>
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

                if (attributes.Length > 0)
                {
                    matchingMethods.Add(method);
                }
            }

            return matchingMethods;
        }

        /// <summary>
        ///     Get all methods of a given object decorated with a T-type attribute.
        /// </summary>
        /// <param name="obj">The object to evaluate methods of.</param>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <returns>A list of all methods of the provided object decorated with the T-type attribute.</returns>
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

    /// <summary>
    ///     Base interface for all custom <see cref="Attribute"/>s in this SDK.
    /// </summary>
    internal interface IBaseCustomAttribute
    {
    }
}
