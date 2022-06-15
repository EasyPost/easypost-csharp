using System;
using System.Reflection;
using EasyPost.Exceptions;
using EasyPost.Interfaces;
using EasyPost.Utilities;

namespace EasyPost.ApiCompatibility
{
    internal static class ApiCompatibilityUtilities
    {
        /// <summary>
        ///     Check if a function (i.e. ShipmentService.Create) is compatible with the current API version.
        /// </summary>
        /// <param name="function">Method attempting to execute.</param>
        /// <param name="client">API client used to execute this method.</param>
        /// <exception cref="ApiVersionNotSupported">When the function is not compatible with the client's API version.</exception>
        internal static void CheckFunctionalityCompatible(MethodInfo function, BaseClient client)
        {
            ApiCompatibilityAttribute? apiCompatibilityAttribute = BaseCustomAttribute.GetCustomAttribute<ApiCompatibilityAttribute>(function);
            if (apiCompatibilityAttribute == null)
            {
                // if property does not have an API compatibility attribute, it is compatible with all API versions
                return;
            }

            // throw exception if property is not compatible with this API version
            if (!apiCompatibilityAttribute.IsCompatible(client.ApiVersion))
            {
                throw new ApiVersionNotSupported(function.Name, client.ApiVersion);
            }
        }

        /// <summary>
        ///     Check if a function (i.e. ShipmentService.Create) is compatible with the current API version.
        /// </summary>
        /// <param name="functionName">Name of method attempting to execute.</param>
        /// <param name="functionSourceType">Type of object the function is being executed on.</param>
        /// <param name="client">API client used to execute this method.</param>
        /// <exception cref="ApiVersionNotSupported">When the function is not compatible with the client's API version.</exception>
        internal static void CheckFunctionalityCompatible(string functionName, Type functionSourceType, BaseClient client)
        {
            MethodInfo? function = functionSourceType.GetMethod(functionName);
            if (function == null)
            {
                throw new ArgumentException($"Could not find method {functionName} on type {functionSourceType.Name}");
            }

            CheckFunctionalityCompatible(function, client);
        }

        /// <summary>
        ///     Check if a service (i.e. ShipmentService) is compatible with the current API version.
        /// </summary>
        /// <param name="service">Service attempting to retrieve.</param>
        /// <param name="client">API client used to retrieve this service.</param>
        /// <exception cref="ApiVersionNotSupported">When the service is not compatible with the client's API version.</exception>
        internal static void CheckServiceCompatible(PropertyInfo service, BaseClient client)
        {
            ApiCompatibilityAttribute? apiCompatibilityAttribute = BaseCustomAttribute.GetCustomAttribute<ApiCompatibilityAttribute>(service);
            if (apiCompatibilityAttribute == null)
            {
                // if property does not have an API compatibility attribute, it is compatible with all API versions
                return;
            }

            // throw exception if property is not compatible with this API version
            if (!apiCompatibilityAttribute.IsCompatible(client.ApiVersion))
            {
                throw new ApiVersionNotSupported(service.Name, client.ApiVersion);
            }
        }

        /// <summary>
        ///     Check if a service (i.e. ShipmentService) is compatible with the current API version.
        /// </summary>
        /// <param name="serviceName">Name of service attempting to retrieve.</param>
        /// <param name="serviceSourceType">Type of object the service is being retrieved from.</param>
        /// <param name="client">API client used to execute this method.</param>
        /// <exception cref="ApiVersionNotSupported">When the service is not compatible with the client's API version.</exception>
        internal static void CheckServiceCompatible(string serviceName, Type serviceSourceType, BaseClient client)
        {
            PropertyInfo? property = serviceSourceType.GetProperty(serviceName);
            if (property == null)
            {
                throw new ArgumentException($"Could not find method {property} on type {serviceSourceType.Name}");
            }

            CheckServiceCompatible(property, client);
        }
    }
}
