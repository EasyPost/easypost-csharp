using System;
using System.Collections;
using System.Reflection;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Utilities;
using MethodDecorator.Fody.Interfaces;

[module: ApiCompatibility]

namespace EasyPost.ApiCompatibility
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal class ApiCompatibilityAttribute : BaseCustomAttribute, IMethodDecorator
    {
        /// <summary>
        ///     The API versions that this property is compatible with.
        /// </summary>
        private ApiVersion[] ApiVersions { get; }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="apiVersions">API versions that is property is compatible with.</param>
        internal ApiCompatibilityAttribute(params ApiVersion[] apiVersions)
        {
            ApiVersions = apiVersions;
        }

        // the below functions are only called when a method (i.e. client.Shipments.Get()) is triggered.
        // getter/setters for services (properties) (i.e. client.Shipments) do not trigger the below functions.
        // instead, API compatibility for services is checked during the GetService() function of the client.
        public void Init(object instance, MethodBase method, object[] args)
        {
            CheckFunctionCompatible(instance, method);
        }

        public void OnEntry()
        {
        }

        public void OnException(Exception exception)
        {
        }

        public void OnExit()
        {
        }

        /// <summary>
        ///     Check if the function tagged with this attribute is compatible with the specified API version.
        /// </summary>
        /// <param name="instance">Instance calling this method.</param>
        /// <param name="method">Method being called.</param>
        /// <exception cref="ArgumentNullException">Instance does not have a Client.</exception>
        /// <exception cref="ApiVersionNotSupportedException">Function is not compatible with the client's API version.</exception>
        private void CheckFunctionCompatible(object instance, MemberInfo method)
        {
            Client? client = ((WithClient)instance).Client;
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            // throw exception if property is not compatible with this API version
            if (!IsCompatible(client.ApiVersionDetails))
            {
                throw new ApiVersionNotSupportedException($"{instance.GetType().Name}.{method.Name}", client.ApiVersionDetails);
            }
        }

        /// <summary>
        ///     Get whether the property is compatible with the specified API version.
        /// </summary>
        /// <param name="apiVersionDetails">Attempted API version.</param>
        /// <returns>True if the property is compatible with the provided API version.</returns>
        private bool IsCompatible(ApiVersionDetails apiVersionDetails)
        {
            return ((IList)ApiVersions).Contains(apiVersionDetails.ApiVersionEnum);
        }

        /// <summary>
        ///     Check if a service (i.e. ShipmentService) is compatible with the current API version.
        /// </summary>
        /// <param name="service">Service attempting to retrieve.</param>
        /// <param name="client">API client used to retrieve this service.</param>
        /// <exception cref="ApiVersionNotSupportedException">Service is not compatible with the client's API version.</exception>
        internal static void CheckServiceCompatible(PropertyInfo service, EasyPostClient? client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            ApiCompatibilityAttribute? apiCompatibilityAttribute = GetCustomAttribute<ApiCompatibilityAttribute>(service);
            if (apiCompatibilityAttribute == null)
            {
                // if property does not have an API compatibility attribute, it is compatible with all API versions
                return;
            }

            // throw exception if property is not compatible with this API version
            if (!apiCompatibilityAttribute.IsCompatible(client.ApiVersionDetails))
            {
                throw new ApiVersionNotSupportedException(service.Name, client.ApiVersionDetails);
            }
        }

        /// <summary>
        ///     Check if a service (i.e. ShipmentService) is compatible with the current API version.
        /// </summary>
        /// <param name="serviceName">Name of service attempting to retrieve.</param>
        /// <param name="serviceSourceType">Type of object the service is being retrieved from.</param>
        /// <param name="client">API client used to execute this method.</param>
        /// <exception cref="ApiVersionNotSupportedException">Service is not compatible with the client's API version.</exception>
        internal static void CheckServiceCompatible(string serviceName, Type serviceSourceType, EasyPostClient? client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            PropertyInfo? property = serviceSourceType.GetProperty(serviceName);
            if (property == null)
            {
                throw new ArgumentException($"Could not find method {property} on type {serviceSourceType.Name}");
            }

            CheckServiceCompatible(property, client);
        }
    }
}
