using System;
using System.Reflection;

namespace EasyPost.Utilities
{
    internal abstract class BaseCustomAttribute : Attribute
    {
        /// <summary>
        ///     Get the attribute for a property.
        /// </summary>
        /// <param name="property">Property to get attribute of.</param>
        /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
        /// <returns>T-type attribute for the property.</returns>
        internal static T? GetCustomAttribute<T>(PropertyInfo property) where T : BaseCustomAttribute
        {
            try
            {
                return property.GetCustomAttribute<T>(true);
            } catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Get the attributes for a property.
        /// </summary>
        /// <param name="property">Property to get attributes of.</param>
        /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
        /// <returns>All T-type attributes for the property.</returns>
        internal static T[]? GetCustomAttributes<T>(PropertyInfo property) where T : BaseCustomAttribute
        {
            object[] attributes = property.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return null;
            }

            return (T[])attributes;
        }

        /// <summary>
        ///     Get the attributes for a method.
        /// </summary>
        /// <param name="method">Method to get attribute of.</param>
        /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
        /// <returns>T-type attribute for the method.</returns>
        internal static T? GetCustomAttribute<T>(MethodInfo method) where T : BaseCustomAttribute
        {
            try
            {
                return method.GetCustomAttribute<T>(true);
            } catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Get the attributes for a method.
        /// </summary>
        /// <param name="method">Method to get attributes of.</param>
        /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
        /// <returns>All T-type attributes for the method.</returns>
        internal static T[]? GetCustomAttributes<T>(MethodInfo method) where T : BaseCustomAttribute
        {
            object[] attributes = method.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return null;
            }

            return (T[])attributes;
        }

        /// <summary>
        ///     Get the attributes for a method.
        /// </summary>
        /// <param name="method">Method to get attribute of.</param>
        /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
        /// <returns>T-type attribute for the method.</returns>
        internal static T? GetCustomAttribute<T>(MethodBase method) where T : BaseCustomAttribute
        {
            try
            {
                return method.GetCustomAttribute<T>(true);
            } catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     Get the attributes for a method.
        /// </summary>
        /// <param name="method">Method to get attributes of.</param>
        /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
        /// <returns>All T-type attributes for the method.</returns>
        internal static T[]? GetCustomAttributes<T>(MethodBase method) where T : BaseCustomAttribute
        {
            object[] attributes = method.GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return null;
            }

            return (T[])attributes;
        }

        internal static bool HasCustomAttribute<T>(PropertyInfo property) where T : BaseCustomAttribute
        {
            return property.GetCustomAttribute<T>(true) != null;
        }

        internal static bool HasCustomAttribute<T>(MethodInfo method) where T : BaseCustomAttribute
        {
            return method.GetCustomAttribute<T>(true) != null;
        }

        internal static bool HasCustomAttribute<T>(MethodBase method) where T : BaseCustomAttribute
        {
            return method.GetCustomAttribute<T>(true) != null;
        }
    }
}
