using System;
using System.Collections;
using System.Reflection;

namespace EasyPost.Clients
{
    internal abstract class BaseAttribute : Attribute
    {
        /// <summary>
        ///     Get the attributes for a class.
        /// </summary>
        /// <param name="type">Class to get attributes of.</param>
        /// <typeparam name="T">Type of attribute to retrieve</typeparam>
        /// <returns>All T-type attributes for the class.</returns>
        public static T[]? GetClassAttributes<T>(Type type) where T : BaseAttribute
        {
            object[] attributes = type.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return null;
            }

            return (T[])attributes;
        }

        /// <summary>
        ///     Get the attributes for a property.
        /// </summary>
        /// <param name="property">Property to get attributes of.</param>
        /// <typeparam name="T">Type of attribute to retrieve</typeparam>
        /// <returns>All T-type attributes for the property.</returns>
        public static T[]? GetPropertyAttributes<T>(PropertyInfo property) where T : BaseAttribute
        {
            object[] attributes = property.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return null;
            }

            return (T[])attributes;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal class ApiCompatibilityAttribute : BaseAttribute
    {
        /// <summary>
        ///     The API versions that this property is compatible with.
        /// </summary>
        private ApiVersion[] ApiVersions { get; }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="apiVersions">API versions that is property is compatible with.</param>
        public ApiCompatibilityAttribute(params ApiVersion[] apiVersions)
        {
            ApiVersions = apiVersions;
        }

        /// <summary>
        ///     Get whether the property is compatible with the specified API version.
        /// </summary>
        /// <param name="apiVersion">Attempted API version.</param>
        /// <returns>True if the property is compatible with the provided API version.</returns>
        public bool IsCompatible(ApiVersion apiVersion)
        {
            return ((IList)ApiVersions).Contains(apiVersion);
        }
    }
}
